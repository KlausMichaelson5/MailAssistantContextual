using MailAssistant.BlazorWebApp.Components.Models;
using MailAssistant.BlazorWebApp.Interfaces;
using MailAssistant.BlazorWebApp.Models;
using MailAssistant.BlazorWebApp.Services;

namespace MailAssistant.BlazorWebApp.Helpers
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
            services.AddRazorComponents()
                    .AddInteractiveServerComponents();

            services.AddTransient(sp => new HttpClient());

            IConfiguration Configuration = ConfigurationHelper.Configuration;
            var apiPaths = Configuration.GetSection("ApiPaths").Get<ApiPaths>();
			services.AddSingleton<ApiPaths>(apiPaths);

			services.AddTransient<IChatUIService, ChatUIService>();
            services.AddTransient<IEmailUIService, EmailUIService>();

            services.AddSingleton<EmailInfoService>();
            services.AddSingleton<EmailOptimizerService>();

            services.AddLogging(configure => configure.AddConsole());

        }
    }
}
