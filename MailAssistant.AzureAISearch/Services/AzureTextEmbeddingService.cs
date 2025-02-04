using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Model.AppSettingsModels;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace MailAssistant.AzureAISearch.Services
{  
    public class AzureTextEmbeddingService : IAzureTextEmbeddingService
    {
        private readonly AppSettings _appSettings;
        public AzureTextEmbeddingService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }
#pragma warning disable SKEXP0010
        public AzureOpenAITextEmbeddingGenerationService GetAzureOpenAITextEmbeddingGenerationService()
        {
            var model = _appSettings.AzureOpenAITextEmbedding.Model;
            var endpoint = _appSettings.AzureOpenAITextEmbedding.Endpoint;

            var client = new SecretClient(new Uri(_appSettings.AzureKeyVault.BaseUrl), new DefaultAzureCredential());
            KeyVaultSecret retrievedSecret = client.GetSecret("AzureOpenAI--ApiKey");
            var apikey = retrievedSecret.Value;

            var textEmbeddingService = new AzureOpenAITextEmbeddingGenerationService(model, endpoint, apikey);

            return textEmbeddingService;

        }
#pragma warning restore SKEXP0050
    }
}
