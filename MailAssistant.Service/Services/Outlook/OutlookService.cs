using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.Helpers.Models;
using MailAssistant.Services.AppSettingsModels;
using MailAssistant.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.VectorData;
using Microsoft.Office.Interop.Outlook;
using Microsoft.SemanticKernel.Embeddings;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace MailAssistant.Services.Services.OutlookServices
{
    public class OutlookService : IOutlookService
    {
        private const string PR_SMTP_ADDRESS = "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";
        private readonly AppSettings _appSettings;
        private Outlook.MAPIFolder _folder;
        private IAzureTextEmbeddingService _azureTextEmbeddingGenerationService;
        private readonly IAzureVectorStoreService _vectorStoreService;
        private Outlook.Application _outlookApp;
        private IVectorStoreRecordCollection<string, Email> _collection;

        public OutlookService(IAzureTextEmbeddingService azureTextEmbeddingGenerationService, IAzureVectorStoreService vectorStoreService,
                 IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _azureTextEmbeddingGenerationService = azureTextEmbeddingGenerationService;
            _vectorStoreService = vectorStoreService;

            var vectorStore = _vectorStoreService.GetAzureAISearchVectorStore();
            _collection = vectorStore.GetCollection<string, Email>("Email");

            _outlookApp = new Outlook.Application();
            MAPIFolder inboxFolder = InitializeOutlookFolder(_outlookApp);
            _folder = inboxFolder.Folders[_appSettings.Outlook.FolderName];
        }

        private MAPIFolder InitializeOutlookFolder(Outlook.Application outlookApp)
        {
            Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");

            // Select the Inbox folder
            Outlook.MAPIFolder inboxFolder = outlookNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            return inboxFolder;
        }

        public async Task<List<Email>> GetMailsFromOutlook(int count = int.MaxValue)
        {
            Outlook.Items outlookMails = _folder.Items;

            // Sort by ReceivedTime from new to old 
            outlookMails.Sort("[ReceivedTime]", true);

            List<Email> emails = new List<Email>();
            for (int i = 1; i <= count && i <= outlookMails.Count; i++)
            {
                Outlook.MailItem outlookMail = outlookMails[i] as Outlook.MailItem;
                if (outlookMail != null)
                {
                    try
                    {
                        Email email = new Email();
                        email.Id = outlookMail.EntryID;
                        email.From = GetSenderEmail(outlookMail);
                        email.To = GetRecipientEmail(outlookMail);
                        email.Subject = outlookMail.Subject;
                        email.Body = outlookMail.Body;
                        email.Date = outlookMail.ReceivedTime.ToShortDateString();
                        emails.Add(email);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return await Task.FromResult(emails);
        }

        private string GetSenderEmail(MailItem outlookMail)
        {
            Outlook.PropertyAccessor senderPropertyAccessor = outlookMail.Sender.PropertyAccessor;
            return senderPropertyAccessor.GetProperty(PR_SMTP_ADDRESS).ToString();
        }

        private string GetRecipientEmail(MailItem outlookMail)
        {
            Outlook.Recipients recipients = outlookMail.Recipients;
            Outlook.PropertyAccessor recipientPropertyAccessor = recipients[1].PropertyAccessor;
            return recipientPropertyAccessor.GetProperty(PR_SMTP_ADDRESS).ToString();
        }

        public async Task GenerateEmbeddingsAndUpsertAsync(int count = int.MaxValue)
        {
            await _collection.CreateCollectionIfNotExistsAsync();
            List<Email> emails = await GetMailsFromOutlook(count);

            foreach (Email email in emails)
            {
                ReadOnlyMemory<float> embedding = await _azureTextEmbeddingGenerationService.GetAzureOpenAITextEmbeddingGenerationService().GenerateEmbeddingAsync(email.ToString());
                email.Embedding = embedding;
                await _collection.UpsertAsync(email);
            }
        }

        public async Task<List<Email>> GenerateEmbeddingsAndSearchAsync(string query, int top = 1, int skip = 0)
        {
            // Generate the embedding.
            ReadOnlyMemory<float> searchEmbedding =
                await _azureTextEmbeddingGenerationService.GetAzureOpenAITextEmbeddingGenerationService().GenerateEmbeddingAsync(query);

            // Do the search, passing an options object with a Top value to limit resulst to the single top match.
            var searchResult = await _collection.VectorizedSearchAsync(searchEmbedding, new() { Top = top, Skip = skip });

            List<Email> emails = new List<Email>();
            await foreach (var record in searchResult.Results)
            {
                if (record.Score > 0.8)
                {
                    emails.Add(record.Record);
                }
            }
            return emails;
        }

        public async Task AddEmailAsync(Email email)
        {
            // Create a new mail item
            Outlook.MailItem outlookMail = (Outlook.MailItem)_outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

            outlookMail.To = email.To;
            outlookMail.Subject = email.Subject;
            outlookMail.Body = email.Body;
            outlookMail.Move(_folder);
        }

        public async Task ReplyToEmailAsync(Email email)
        {
            // Get email by ID
            Outlook.NameSpace outlookNamespace = _outlookApp.GetNamespace("MAPI");
            Outlook.MailItem mail = (Outlook.MailItem)outlookNamespace.GetItemFromID(email.Id);

            // Reply to the mail item
            Outlook.MailItem reply = mail.Reply();

            reply.To = GetRecipientEmail(mail);
            reply.Subject = "RE: " + mail.Subject;
            reply.Body = email.Body;
            reply.Move(_folder);
        }
    }
}
