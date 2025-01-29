using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MailAssistant.Contracts.Interfaces;
using MailAssistant.Services.Helpers;
using Microsoft.SemanticKernel;
using Kernel = Microsoft.SemanticKernel.Kernel;

namespace MailAssistant.Services.Services
{
	/// <summary>
	/// Represents a factory for creating Azure OpenAI kernels that implements IKernelFactory.
	/// </summary>
	public class AzureOpenAIKernel : IKernelFactory
    {
		/// <summary>
		/// Creates a new kernel instance configured for Azure OpenAI.
		/// </summary>
		/// <returns>A new instance of the <see cref="Kernel"/> class.</returns>
		public Kernel CreateKernel()
        {
            var configuration = ConfigurationHelper.Configuration;

            var model = configuration["AzureOpenAI:Model"];
            var endpoint = configuration["AzureOpenAI:Endpoint"];

            var client = new SecretClient(new Uri(configuration["AzureKeyVault:BaseUrl"]), new DefaultAzureCredential());
            KeyVaultSecret retrievedSecret = client.GetSecret("AzureOpenAI--ApiKey");
            var apikey = retrievedSecret.Value;

            IKernelBuilder builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(model, endpoint, apikey);

            var kernel = builder.Build();
            return kernel;
        }
    }
}
