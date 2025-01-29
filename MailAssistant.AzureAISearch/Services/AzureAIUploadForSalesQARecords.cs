using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Model;
using Microsoft.SemanticKernel.Embeddings;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureAIUploadForSalesQARecords
    {
        #pragma warning disable SKEXP0010
        private readonly IAzureTextEmbeddingService _embeddingGenerationService;
        #pragma warning restore SKEXP0010
        private readonly IAzureVectorStoreService _vectorStoreService;

        public AzureAIUploadForSalesQARecords(IAzureTextEmbeddingService embeddingService, IAzureVectorStoreService vectorStoreService)
        {
            _embeddingGenerationService = embeddingService;
            _vectorStoreService = vectorStoreService;
        }
        public async void UploadData(string salesQandA)
        {
            var textEmbeddingService = _embeddingGenerationService.GetAzureOpenAITextEmbeddingGenerationService();
            var vectorStore = _vectorStoreService.GetAzureAISearchVectorStore();

            var collection = vectorStore.GetCollection<string, SalesQARecords>("salesqaformailgen");
            await collection.CreateCollectionIfNotExistsAsync();

            ReadOnlyMemory<float> embedding = await textEmbeddingService.GenerateEmbeddingAsync(salesQandA);
            await collection.UpsertAsync(new SalesQARecords
            {
                SalesQandA = salesQandA,
                SalesQandAEmbedding = embedding
            });
        }
    }
}
