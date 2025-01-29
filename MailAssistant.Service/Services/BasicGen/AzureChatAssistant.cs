using MailAssistant.Contracts.Interfaces;
using MailAssistant.Helpers.KernelFunction;
using MailAssistant.Services.Helpers;
using MailAssistant.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;


namespace MailAssistant.Services.Services
{
    /// <summary>
    /// Represents an Azure-based chat assistant that implements the IChatService interface.
    /// </summary>
    public class AzureChatAssistant : IChatService
	{
		private readonly IKernelFactory _kernelFactory;
		private readonly ISettingsFactory _settingsFactory;
		private readonly ILogger<AzureChatAssistant> _logger;

		private static readonly ChatHistory chatHistory = new ChatHistory();
		private static readonly IConfiguration configuration = ConfigurationHelper.Configuration;
        private static readonly string systemPrompt = File.ReadAllText(configuration["SystemChatMessages:AzureChatAssistant"]);

        private readonly Kernel kernel;
        private readonly OpenAIPromptExecutionSettings openAIPromptExecutionSettings;

		/// <summary>
		/// Initializes a new instance of the <see cref="AzureChatAssistant"/> class.
		/// </summary>
		/// <param name="kernelFactory">The factory to create kernel instances.</param>
		/// <param name="settingsFactory">The factory to create settings instances.</param>
		public AzureChatAssistant(IKernelFactory kernelFactory, ISettingsFactory settingsFactory, ILogger<AzureChatAssistant> logger)
		{
			_kernelFactory = kernelFactory;
			_settingsFactory = settingsFactory;
			_logger = logger;

            chatHistory.AddSystemMessage(systemPrompt);

			kernel = _kernelFactory.CreateKernel();
			kernel.Plugins.AddFromType<EmailDraftingPlugin>("EmailDrafting");
			kernel.Plugins.AddFromType<EmailAssistantPlugin>("EmailAssistant");
			#pragma warning disable SKEXP0050
            kernel.Plugins.AddFromType<TimePlugin>("TimePlugin");
			#pragma warning restore SKEXP0050
            openAIPromptExecutionSettings = _settingsFactory.CreateSettings();

        }

		/// <summary>
		/// Gets the response from the chat assistant.
		/// </summary>
		/// <param name="inputMessage">The input message from the user.</param>
		/// <param name="newChat">Indicates whether this is a new chat session.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the azure based openAI assistant's response.</returns>
		public async Task<string> GetChatAssistantResponse(string inputMessage, bool newChat)
		{
			if (newChat)
			{
				chatHistory.Clear();
                chatHistory.AddSystemMessage(systemPrompt);

            }

			var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
			chatHistory.AddUserMessage(inputMessage);
			var assistantReply = string.Empty;

			try
			{
				var chatCompletionResult = await chatCompletionService.GetChatMessageContentAsync(chatHistory, openAIPromptExecutionSettings, kernel);
				assistantReply = $"{chatCompletionResult}";
                _logger.LogInformation("Got reply from assistant.");

            }
			catch (Exception ex) when (ex.Message.Contains("exceeded token rate limit"))
			{
				assistantReply = $"Rate limit exceeded, please wait and {ExtractMessageHelper.ExtractRetryAfterSeconds(ex.Message)}";
                _logger.LogError($"Got exception from assistant.{assistantReply}");

            }
            catch (Exception)
			{
				assistantReply = $"Internal server error.Please try again later";
                _logger.LogError("Got Internal error.");

            }

            return assistantReply;
		}
	}

}
