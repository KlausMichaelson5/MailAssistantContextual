using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace MailAssistant.AzureAISearch.Interfaces
{
    public interface IAzureTextEmbeddingService
    {
        #pragma warning disable SKEXP0010
        AzureOpenAITextEmbeddingGenerationService GetAzureOpenAITextEmbeddingGenerationService();
        #pragma warning restore SKEXP0050

    }
}