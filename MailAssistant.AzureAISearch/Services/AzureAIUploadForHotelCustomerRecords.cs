using System.Text.Json;
using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.Helpers.Models.Hotel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Embeddings;

namespace MailAssistant.AzureAISearch.Services
{
    public class AzureAIUploadForHotelCustomerRecords
    {
        #pragma warning disable SKEXP0010
        private readonly IAzureTextEmbeddingService _embeddingGenerationService;
        #pragma warning restore SKEXP0010
        private readonly IAzureVectorStoreService _vectorStoreService;

        private readonly ILogger<AzureAIUploadForHotelCustomerRecords> _logger;

        public AzureAIUploadForHotelCustomerRecords(IAzureTextEmbeddingService embeddingService, IAzureVectorStoreService vectorStoreService,ILogger<AzureAIUploadForHotelCustomerRecords> logger)
        {
            _embeddingGenerationService = embeddingService;
            _vectorStoreService = vectorStoreService;
            _logger = logger;
        }
        public async Task UploadData(HotelCustomer customer)
        {
            try
            {
                var textEmbeddingService = _embeddingGenerationService.GetAzureOpenAITextEmbeddingGenerationService();
                var vectorStore = _vectorStoreService.GetAzureAISearchVectorStore();

                var collection = vectorStore.GetCollection<string, HotelCustomerVector>("hotelcustomer");
                await collection.CreateCollectionIfNotExistsAsync();

                ReadOnlyMemory<float> embedding = await textEmbeddingService.GenerateEmbeddingAsync(customer.BookingHistory.ToString());
                HotelCustomerVector hotelCustomerVector = new HotelCustomerVector
                {
                    HotelCustomerId = customer.HotelCustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    BookingHistory = customer.BookingHistory
                                        .Select(b => JsonSerializer.Serialize(b))
                                        .ToList(),
                    BookingHistoryEmbedding = embedding

                };
                await collection.UpsertAsync(hotelCustomerVector);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
