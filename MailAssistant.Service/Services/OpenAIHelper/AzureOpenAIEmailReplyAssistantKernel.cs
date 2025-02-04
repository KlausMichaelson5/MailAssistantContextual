using MailAssistant.Helpers.KernelFunction;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel;
using MailAssistant.Services.Interfaces;
using MailAssistant.AzureAISearch.Model;
using Microsoft.SemanticKernel.Data;
using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.Services.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace MailAssistant.Services.Services.OpenAIHelper
{
    public class AzureOpenAIEmailReplyAssistantKernel : IKernelFactory
    {
        private readonly Kernel kernel;
        #pragma warning disable SKEXP0010
        private readonly IAzureTextEmbeddingService _azureTextEmbeddingGenerationService;
        #pragma warning restore SKEXP0010
        private readonly IAzureVectorStoreService _vectorStoreService;
        private readonly WebSearchPlugin _webSearchPlugin;
        private readonly ILogger<AzureOpenAIEmailReplyAssistantKernel> _logger;


        public AzureOpenAIEmailReplyAssistantKernel([FromKeyedServices("Base")] IKernelFactory kernelFactory, IAzureTextEmbeddingService azureTextEmbeddingGenerationService, IAzureVectorStoreService vectorStoreService, WebSearchPlugin webSearchPlugin, ILogger<AzureOpenAIEmailReplyAssistantKernel> logger)
        {
             kernel = kernelFactory.GetKernel();
            _azureTextEmbeddingGenerationService = azureTextEmbeddingGenerationService;
            _vectorStoreService = vectorStoreService;
            _webSearchPlugin = webSearchPlugin;
            _logger = logger;

        }

        public Kernel GetKernel()
        {

            kernel.Plugins.AddFromType<EmailDraftingPlugin>("EmailDrafting");
            kernel.Plugins.AddFromType<EmailAssistantPlugin>("EmailAssistant");
            #pragma warning disable SKEXP0050
            kernel.Plugins.AddFromType<TimePlugin>("TimePlugin");
            #pragma warning restore SKEXP0050

            AddTextSearchPluginForSalesQARecord().Wait();
            kernel.ImportPluginFromObject(_webSearchPlugin.GetAzureBingSearchPlugin(), "BingPlugin");

            return kernel;
        }

        private async Task AddTextSearchPluginForSalesQARecord()
        {
            var textEmbeddingGenerationService = _azureTextEmbeddingGenerationService.GetAzureOpenAITextEmbeddingGenerationService();
            var vectorStore = _vectorStoreService.GetAzureAISearchVectorStore();
            var collection = vectorStore.GetCollection<string, SalesQARecords>("salesqaformailgen");
            await collection.CreateCollectionIfNotExistsAsync();
              #pragma warning disable SKEXP0001
            var textSearch = new VectorStoreTextSearch<SalesQARecords>(collection, textEmbeddingGenerationService);
            #pragma warning restore SKEXP0001
            var searchPlugin = textSearch.CreateWithGetTextSearchResults("AzureAISearchPlugin");
            kernel.Plugins.Add(searchPlugin);
        }

    }
}
