using Azure;
using Azure.Identity;
using Azure.Search.Documents.Indexes;
using Azure.Security.KeyVault.Secrets;
using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Model.AppSettingsModels;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureVectorStoreService : IAzureVectorStoreService
    {

        private readonly AppSettings _appSettings;
        public AzureVectorStoreService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }
        public  AzureAISearchVectorStore GetAzureAISearchVectorStore()
        {
            var endpoint = _appSettings.AzureAISearch.Endpoint;

            var client = new SecretClient(new Uri(_appSettings.AzureKeyVault.BaseUrl), new DefaultAzureCredential());
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
