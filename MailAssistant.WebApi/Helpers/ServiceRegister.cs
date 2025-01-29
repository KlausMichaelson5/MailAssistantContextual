using MailAssistant.AzureAISearch.Interfaces;
using MailAssistant.AzureAISearch.Services;
using MailAssistant.Contracts.Interfaces;
using MailAssistant.Services.Interfaces;
using MailAssistant.Services.Services;
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
            services.AddTransient<IEmailDataService, EmailDataService>();

            services.AddTransient<IKernelFactory, AzureOpenAIKernel>();
            services.AddTransient<ISettingsFactory, OpenAIFunctionChoiceRequired>();

            services.AddTransient<IAzureTextEmbeddingService, AzureTextEmbeddingService>();
            services.AddTransient<IAzureVectorStoreService, AzureVectorStoreService>();            

            services.AddSingleton<IChatService, AzureContextualEmailReplyGenAssistant>();
            services.AddSingleton<IEmailService, AzureEmailAssistant>();

            services.Configure<ApiBehaviorOptions>(options
                  => options.SuppressModelStateInvalidFilter = true);

            services.AddLogging(configure => configure.AddConsole());
        }
    }
}
