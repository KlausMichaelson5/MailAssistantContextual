using Azure;
using Azure.Identity;
using Azure.Search.Documents.Indexes;
using Azure.Security.KeyVault.Secrets;
using MailAssistant.AzureAISearch.Helpers;
using MailAssistant.AzureAISearch.Interfaces;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureVectorStoreService : IAzureVectorStoreService
    {
        public  AzureAISearchVectorStore GetAzureAISearchVectorStore()
        {
            var configuration = ConfigurationHelper.Configuration;
            var endpoint = configuration["AzureAISearch:Endpoint"];

            var client = new SecretClient(new Uri(configuration["AzureKeyVault:BaseUrl"]), new DefaultAzureCredential());
            KeyVaultSecret retrievedSecret = client.GetSecret("AzureAIsearch--ApiKey");
            var apikey = retrievedSecret.Value;

            var vectorStore = new AzureAISearchVectorStore(
               new SearchIndexClient(
                   new Uri(endpoint),
                   new AzureKeyCredential(apikey)));

            return vectorStore;

        }
    }
}
