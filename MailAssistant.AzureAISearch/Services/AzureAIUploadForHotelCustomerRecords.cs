using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Models;
using Microsoft.SemanticKernel.Embeddings;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureAIUploadForHotelCustomerRecords
    {
        #pragma warning disable SKEXP0010
        private readonly IAzureTextEmbeddingService _embeddingGenerationService;
        #pragma warning restore SKEXP0010
        private readonly IAzureVectorStoreService _vectorStoreService;

        public AzureAIUploadForHotelCustomerRecords(IAzureTextEmbeddingService embeddingService, IAzureVectorStoreService vectorStoreService)
        {
            _embeddingGenerationService = embeddingService;
            _vectorStoreService = vectorStoreService;
        }
        public async void UploadData(HotelCustomer customer)
        {
            var textEmbeddingService = _embeddingGenerationService.GetAzureOpenAITextEmbeddingGenerationService();
            var vectorStore = _vectorStoreService.GetAzureAISearchVectorStore();

            var collection = vectorStore.GetCollection<string, HotelCustomer>("hotelcustomer");
            await collection.CreateCollectionIfNotExistsAsync();

            ReadOnlyMemory<float> embedding = await textEmbeddingService.GenerateEmbeddingAsync(customer.BookingHistory.ToString());
            customer.BookingHistoryEmbedding= embedding;
            await collection.UpsertAsync(customer);
        }
    }
}
