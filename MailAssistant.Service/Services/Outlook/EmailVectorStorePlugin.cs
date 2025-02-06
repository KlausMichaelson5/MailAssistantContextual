using MailAssistant.Helpers.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Data;
using System.ComponentModel;


namespace MailAssistant.Services.Services.Outlook
{
    public class EmailVectorStorePlugin
    {
        #pragma warning disable SKEXP0001 
        private VectorStoreTextSearch<Email> _emailVectors;
        public EmailVectorStorePlugin(VectorStoreTextSearch<Email> emailVectors)
        {
            _emailVectors = emailVectors;
        }

        [KernelFunction("get_all_emails")]
        [Description("Gets all emails in the collection")]
        public async Task<List<Email>> GetAllEmails()
        {
            List<Email> emails = [];
            var query = "get all emails";
            KernelSearchResults<object> emailResults = await _emailVectors.GetSearchResultsAsync(query);
            await foreach (Email result in emailResults.Results)
            {
                Email email = new Email();
                email.From = result.From;
                email.To = result.To;
                email.Subject = result.Subject;
                email.Date = result.Date;
                email.Body = result.Body;
                emails.Add(email);
            }
            return emails;
        }
        #pragma warning restore SKEXP0001

    }
}