using MailAssistant.Services.Interfaces;
using MailAssistant.Services.Models.AppSettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;


namespace MailAssistant.Services.Services
{
    /// <summary>
    /// Represents a factory for creating Azure OpenAI kernels that implements IKernelFactory.
    /// </summary>
    public class AzureOpenAIKernel : IKernelFactory
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public AzureOpenAIKernel(IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }
        /// <summary>
        /// Creates a new _emailReplyAssistantKernel instance configured for Azure OpenAI.
        /// </summary>
        /// <returns>A new instance of the <see cref="Kernel"/> class.</returns>
        public Kernel GetKernel()
        {
            var model = _appSettings.AzureOpenAI.Model;
            var endpoint =_appSettings.AzureOpenAI.Endpoint;

            //var client = new SecretClient(new Uri(_appSettings.AzureKeyVault.BaseUrl), new DefaultAzureCredential());
            //KeyVaultSecret retrievedSecret = client.GetSecret("AzureOpenAI--ApiKey");
            var apikey = _configuration["AzureOpenAiKey"];

            IKernelBuilder builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(model, endpoint, apikey);

            var kernel = builder.Build();
            return kernel;
        }

    }
}
