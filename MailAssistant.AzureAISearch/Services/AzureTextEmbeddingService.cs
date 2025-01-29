using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MailAssistant.AzureAISearch.Helpers;
using MailAssistant.AzureAISearch.Interfaces;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureTextEmbeddingService : IAzureTextEmbeddingService
    {
#pragma warning disable SKEXP0010
        public AzureOpenAITextEmbeddingGenerationService GetAzureOpenAITextEmbeddingGenerationService()
        {
            var configuration = ConfigurationHelper.Configuration;
            var model = configuration["AzureOpenAITextEmbedding:Model"];
            var endpoint = configuration["AzureOpenAITextEmbedding:Endpoint"];

            var client = new SecretClient(new Uri(configuration["AzureKeyVault:BaseUrl"]), new DefaultAzureCredential());
            KeyVaultSecret retrievedSecret = client.GetSecret("AzureOpenAI--ApiKey");
            var apikey = retrievedSecret.Value;

            var textEmbeddingService = new AzureOpenAITextEmbeddingGenerationService(model, endpoint, apikey);

            return textEmbeddingService;

        }
#pragma warning restore SKEXP0050
    }
}
