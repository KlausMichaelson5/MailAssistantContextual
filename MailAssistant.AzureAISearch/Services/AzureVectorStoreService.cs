using Azure;
using Azure.Search.Documents.Indexes;
using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Models.AppSettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureVectorStoreService : IAzureVectorStoreService
    {

        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public AzureVectorStoreService(IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;

        }
        public  AzureAISearchVectorStore GetAzureAISearchVectorStore()
        {
            var endpoint = _appSettings.AzureAISearch.Endpoint;

            //var client = new SecretClient(new Uri(_appSettings.AzureKeyVault.BaseUrl), new DefaultAzureCredential());
            //KeyVaultSecret retrievedSecret = client.GetSecret("AzureAIsearch--ApiKey");
            var apikey = _configuration["AzureAISearchKey"];
            var vectorStore = new AzureAISearchVectorStore(
               new SearchIndexClient(
                   new Uri(endpoint),
                   new AzureKeyCredential(apikey)));

            return vectorStore;

        }
    }
}
