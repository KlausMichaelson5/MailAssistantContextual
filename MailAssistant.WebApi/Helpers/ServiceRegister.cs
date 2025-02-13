using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Services;
using MailAssistant.Services.Helpers;
using MailAssistant.Services.Interfaces;
using MailAssistant.Services.Services;
using MailAssistant.Services.Services.OpenAIHelper;
using MailAssistant.Services.Services.OutlookServices;
using MailAssistant.Services.Services.PluginIntegrationHelper;
using MailAssistant.WebApi.Interfaces;
using MailAssistant.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailAssistant.WebApi.Helpers
{
    /// <summary>
    /// The <c>ServiceRegistrar</c> class is responsible for registering services
    /// for dependency injection in the application.
    /// </summary>
    internal static class ServiceRegistrar
    {
        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection to which services are added.</param>
        internal static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IChatDataService, ChatDataService>();
            services.AddTransient<IOutlookDataService, OutlookDataService>();

            services.AddKeyedTransient<IKernelFactory, AzureOpenAIKernel>("Base");
            services.AddTransient<IAzureTextEmbeddingService, AzureTextEmbeddingService>();
            services.AddTransient<IAzureVectorStoreService, AzureVectorStoreService>();
            services.AddSingleton<WebSearchPlugin>();
            
            services.AddKeyedTransient<IKernelFactory, AzureOpenAIEmailReplyAssistantKernel>("EmailReplyGen");
            services.AddSingleton<IChatService, AzureContextualEmailReplyGenAssistant>();
            services.AddSingleton<IOutlookService, OutlookService>();

            services.AddSingleton<AzureVectorStorePluginAdder>();

            services.Configure<ApiBehaviorOptions>(options
                  => options.SuppressModelStateInvalidFilter = true);

            services.AddLogging(configure => configure.AddConsole());
        }
    }
}
