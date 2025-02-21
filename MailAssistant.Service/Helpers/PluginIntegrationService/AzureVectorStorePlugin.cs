using MailAssistant.Helpers.Models;
using MailAssistant.Services.Services.Outlook;
using Microsoft.SemanticKernel.Data;
using Microsoft.SemanticKernel;
using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.Helpers.KernelFunction;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using MailAssistant.Helpers.Models.Hotel;

namespace MailAssistant.Services.Helpers.PluginIntegrationService
{
    public class AzureVectorStorePlugin
    {
#pragma warning disable SKEXP0010
        private readonly IAzureTextEmbeddingService _azureTextEmbeddingGenerationService;
        private readonly IAzureVectorStoreService _vectorStoreService;

        private readonly AzureOpenAITextEmbeddingGenerationService _textEmbeddingGenerationService;
        private readonly AzureAISearchVectorStore _vectorStore;

        public AzureVectorStorePlugin(IAzureTextEmbeddingService azureTextEmbeddingGenerationService, IAzureVectorStoreService vectorStoreService)
        {
            _azureTextEmbeddingGenerationService = azureTextEmbeddingGenerationService;
            _vectorStoreService = vectorStoreService;

            _textEmbeddingGenerationService = _azureTextEmbeddingGenerationService.GetAzureOpenAITextEmbeddingGenerationService();
            _vectorStore = _vectorStoreService.GetAzureAISearchVectorStore();
        }

        public async Task AddEmailVectorStorePlugin(Kernel kernel)
        {

            var emailsCollection = _vectorStore.GetCollection<string, Email>("email");
            await emailsCollection.CreateCollectionIfNotExistsAsync();

#pragma warning disable SKEXP0001
            var emailsTextSearch = new VectorStoreTextSearch<Email>(emailsCollection, _textEmbeddingGenerationService);
#pragma warning restore SKEXP0001

            var emailVectorStorePlugin = new EmailVectorStorePlugin(emailsTextSearch);
            kernel.Plugins.AddFromObject(emailVectorStorePlugin, "EmailVectorStorePlugin");
        }

        public async Task AddHotelVectorStorePlugin(Kernel kernel)
        {

            var hotelCustomerCollection = _vectorStore.GetCollection<string, HotelCustomerVector>("hotelcustomer");
            await hotelCustomerCollection.CreateCollectionIfNotExistsAsync();

#pragma warning disable SKEXP0001
            var hotelCustomerTextSearch = new VectorStoreTextSearch<HotelCustomerVector>(hotelCustomerCollection, _textEmbeddingGenerationService);
#pragma warning restore SKEXP0001

            var hotelCustomerVectorStorePlugin = new HotelVectorStorePlugin(hotelCustomerTextSearch); 
            kernel.Plugins.AddFromObject(hotelCustomerVectorStorePlugin, "HotelVectorStorePlugin");

            //var searchPlugin = hotelCustomerTextSearch.CreateWithGetTextSearchResults("HotelClientPlugin"); 
            //kernel.Plugins.Add(searchPlugin);
        }
    }
}
