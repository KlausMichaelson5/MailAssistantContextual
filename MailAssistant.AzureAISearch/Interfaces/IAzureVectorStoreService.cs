using Microsoft.SemanticKernel.Connectors.AzureAISearch;

namespace MailAssistant.AzureAISearch.Interfaces
{
    public interface IAzureVectorStoreService
    {
        AzureAISearchVectorStore GetAzureAISearchVectorStore();
    }
}