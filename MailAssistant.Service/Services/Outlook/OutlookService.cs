using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.Helpers.Models;
using MailAssistant.Services.Interfaces;
using MailAssistant.Services.Models.AppSettingsModels;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.VectorData;
using Microsoft.Office.Interop.Outlook;
using Microsoft.SemanticKernel.Embeddings;

namespace MailAssistant.Services.Services.OutlookServices
{
    public class OutlookService : IOutlookService
    {
        private readonly string PR_SMTP_ADDRESS ;
        private readonly AppSettings _appSettings;
        private readonly MAPIFolder _folder;
        private readonly Application _outlookApp;

        private readonly IAzureTextEmbeddingService _azureTextEmbeddingGenerationService;
        private readonly IAzureVectorStoreService _vectorStoreService;
        private readonly IVectorStoreRecordCollection<string, Email> _collection;

        public OutlookService(IAzureTextEmbeddingService azureTextEmbeddingGenerationService, IAzureVectorStoreService vectorStoreService, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _azureTextEmbeddingGenerationService = azureTextEmbeddingGenerationService;
            _vectorStoreService = vectorStoreService;

            PR_SMTP_ADDRESS = _appSettings.Outlook.PR_SMTP_ADDRESS;

            var vectorStore = _vectorStoreService.GetAzureAISearchVectorStore();
            _collection = vectorStore.GetCollection<string, Email>("Email");

            _outlookApp = new Application();
            var inboxFolder = _outlookApp.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderInbox); ;
            _folder = inboxFolder.Folders[_appSettings.Outlook.FolderName];
        }

        public async Task<List<Email>> GetMailsFromOutlook(int count = int.MaxValue)
        {
            Items outlookMails = _folder.Items;

            // Sort by ReceivedTime from new to old 
            outlookMails.Sort("[ReceivedTime]", true);

            List<Email> emails = [];
            for (int i = 1; i <= count && i <= outlookMails.Count; i++)
            {
                if (outlookMails[i] is MailItem outlookMail)
                {
                    try
                    {
                        Email email = new Email
                        {
                            Id = outlookMail.EntryID,
                            From = GetSenderEmail(outlookMail),
                            To = GetRecipientEmail(outlookMail),
                            Subject = outlookMail.Subject,
                            Body = outlookMail.Body,
                            Date = outlookMail.ReceivedTime.ToShortDateString()
                        };
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

        private  string GetSenderEmail(MailItem outlookMail)
        {
            PropertyAccessor senderPropertyAccessor = outlookMail.Sender.PropertyAccessor;
            return senderPropertyAccessor.GetProperty(PR_SMTP_ADDRESS).ToString();
        }

        private  string GetRecipientEmail(MailItem outlookMail)
        {
            Recipients recipients = outlookMail.Recipients;
            PropertyAccessor recipientPropertyAccessor = recipients[1].PropertyAccessor;
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
    }
}
