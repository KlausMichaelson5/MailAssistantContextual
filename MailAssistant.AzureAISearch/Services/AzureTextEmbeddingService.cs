using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Models.AppSettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureTextEmbeddingService : IAzureTextEmbeddingService
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public AzureTextEmbeddingService(IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }
#pragma warning disable SKEXP0010
        public AzureOpenAITextEmbeddingGenerationService GetAzureOpenAITextEmbeddingGenerationService()
        {
            var model = _appSettings.AzureOpenAITextEmbedding.Model;
            var endpoint = _appSettings.AzureOpenAITextEmbedding.Endpoint;

            //var client = new SecretClient(new Uri(_appSettings.AzureKeyVault.BaseUrl), new DefaultAzureCredential());
            //KeyVaultSecret retrievedSecret = client.GetSecret("AzureOpenAI--ApiKey");
            var apikey = _configuration["AzureOpenAiKey"];
            var textEmbeddingService = new AzureOpenAITextEmbeddingGenerationService(model, endpoint, apikey);

            return textEmbeddingService;

        }
#pragma warning restore SKEXP0050
    }
}
