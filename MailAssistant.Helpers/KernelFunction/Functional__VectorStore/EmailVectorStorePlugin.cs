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

        [KernelFunction("search_emails_in_vector_store_collection")]
        [Description("Search and retrive all emails that match the query from Azure AI search collection")]
        public async Task<List<TextSearchResult>> GetAllEmails(string query)
        {
            List<TextSearchResult> emails = [];
            var emailResults = await _emailVectors.GetTextSearchResultsAsync(query);
            await foreach (var result in emailResults.Results)
            {
                var email = new TextSearchResult(value: result.Value)
                {
                    Name = result.Name,
                    Link = result.Link
                };
                emails.Add(email);
            }
            return emails;
        }
        #pragma warning restore SKEXP0001

    }
}