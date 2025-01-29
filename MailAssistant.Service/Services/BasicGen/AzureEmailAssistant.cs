using MailAssistant.Contracts.Interfaces;
using MailAssistant.Helpers.KernelFunction;
using MailAssistant.Services.Helpers;
using MailAssistant.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;

namespace MailAssistant.Services.Services
{
    /// <summary>
    /// Represents an Azure-based email assistant that implements the IEmailService interface.
    /// </summary>
    public class AzureEmailAssistant : IEmailService
    {
        private readonly IKernelFactory _kernelFactory;
        private readonly ISettingsFactory _settingsFactory;
        private readonly ILogger<AzureEmailAssistant> _logger;

        private readonly Kernel kernel;
        private readonly OpenAIPromptExecutionSettings openAIPromptExecutionSettings;

        private static readonly IConfiguration configuration = ConfigurationHelper.Configuration;
        private static readonly string emailPluginDirectoryPath = configuration["PluginPaths:PluginFunctionsPath"];
        private static readonly string systemPrompt = File.ReadAllText(configuration["SystemChatMessages:AzureEmailAssistant"]);



        /// <summary>
        /// Initializes a new instance of the <see cref="AzureEmailAssistant"/> class.
        /// </summary>
        /// <param name="kernelFactory">The factory to create kernel instances.</param>
        /// <param name="settingsFactory">The factory to create settings instances.</param>
        public AzureEmailAssistant(IKernelFactory kernelFactory, ISettingsFactory settingsFactory, ILogger<AzureEmailAssistant> logger)
        {
            _kernelFactory = kernelFactory;
            _settingsFactory = settingsFactory;
            _logger = logger;

            kernel = _kernelFactory.CreateKernel();
            openAIPromptExecutionSettings = _settingsFactory.CreateSettings();
            kernel.Plugins.AddFromType<EmailDraftingPlugin>("EmailDrafting");
            #pragma warning disable SKEXP0050
            kernel.Plugins.AddFromType<TimePlugin>("TimePlugin");
            #pragma warning restore SKEXP0050

        }

        /// <summary>
        /// Gets a draft email from the assistant based on the user's request.
        /// </summary>
        /// <param name="userRequest">The user's request for the email draft.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the drafted email by azure based openAI assistant based on user request.</returns>

        public async Task<string> GetAssistantDraftEmail(string userRequest)
        {           
			var result=string.Empty;

			try
			{
                var emailFunction=kernel.CreateFunctionFromPrompt(systemPrompt, openAIPromptExecutionSettings);
                var kernelArguments = new KernelArguments()
                {
                    ["input"] = userRequest
                };
                var response = await emailFunction.InvokeAsync(kernel,kernelArguments);
                result=response.ToString();
                _logger.LogInformation("Got reply from assistant.");

            }
            catch (Exception ex) when (ex.Message.Contains("exceeded token rate limit"))
			{
				result = $"Rate limit exceeded, please wait and {ExtractMessageHelper.ExtractRetryAfterSeconds(ex.Message)}";
                _logger.LogError($"Got exception from assistant.{result}");
            }
            catch (Exception ex)
            {
                result = $"Internal server error.Please try again later";
                _logger.LogError($"Got Internal error.{ex.Message}");
            }

            return $"{result}";
        }
    }
}