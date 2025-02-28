using MailAssistant.Helpers.KernelFunction;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel;
using MailAssistant.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MailAssistant.Services.Helpers.PluginIntegrationService;

namespace MailAssistant.Services.Services.OpenAIHelper
{
    public class AzureOpenAIEmailReplyAssistantKernel : IKernelFactory
    {
        private readonly Kernel kernel;
        private readonly WebSearchPlugin _webSearchPlugin;
        private readonly ILogger<AzureOpenAIEmailReplyAssistantKernel> _logger;
        private readonly AzureVectorStorePlugin _azureVectorStorePluginAdder;


        public AzureOpenAIEmailReplyAssistantKernel([FromKeyedServices("Base")] IKernelFactory kernelFactory,AzureVectorStorePlugin azureVectorStorePluginAdder, WebSearchPlugin webSearchPlugin, ILogger<AzureOpenAIEmailReplyAssistantKernel> logger)
        {
             kernel = kernelFactory.GetKernel();
            _azureVectorStorePluginAdder = azureVectorStorePluginAdder;
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

            _azureVectorStorePluginAdder.AddEmailVectorStorePlugin(kernel).Wait();
            _azureVectorStorePluginAdder.AddHotelVectorStorePlugin(kernel).Wait();
            kernel.ImportPluginFromObject(_webSearchPlugin.GetAzureBingSearchPlugin(), "BingPlugin");

            return kernel;
        }

    }
}
