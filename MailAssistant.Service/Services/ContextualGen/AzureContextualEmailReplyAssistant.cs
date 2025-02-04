using MailAssistant.Services.AppSettingsModels;
using MailAssistant.Services.Helpers;
using MailAssistant.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;


namespace MailAssistant.Services.Services
{
    /// <summary>
    /// Represents an Azure-based chat assistant that implements the IChatService interface.
    /// </summary>
    public class AzureContextualEmailReplyGenAssistant : IChatService
    {
        private readonly Kernel _emailReplyAssistantKernel;
        private readonly ILogger<AzureContextualEmailReplyGenAssistant> _logger;
        private readonly AppSettings _appSettings;


        private static readonly ChatHistory chatHistory = new ChatHistory();
        private readonly string systemPrompt = string.Empty;
        private readonly OpenAIPromptExecutionSettings openAIPromptExecutionSettings;


        /// <summary>
        /// Initializes a new instance of the <see cref="AzureContextualEmailReplyGenAssistant"/> class.
        /// </summary>
        /// <param name="emailReplyAssistantKernel">The factory to create _emailReplyAssistantKernel instances.</param>
        public  AzureContextualEmailReplyGenAssistant([FromKeyedServices("EmailReplyGen")] IKernelFactory emailReplyAssistantKernel, ILogger<AzureContextualEmailReplyGenAssistant> logger, IOptions<AppSettings> appSettings)
        {

            _emailReplyAssistantKernel=emailReplyAssistantKernel.GetKernel();
            _logger = logger;
            _appSettings = appSettings.Value;


            systemPrompt = File.ReadAllText(_appSettings.SystemChatMessages.AzureContextualReplyGenAssistant);
            chatHistory.AddSystemMessage(systemPrompt);


            openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Required()
            };

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

            var chatCompletionService = _emailReplyAssistantKernel.GetRequiredService<IChatCompletionService>();
            chatHistory.AddUserMessage(inputMessage);
            var assistantReply = string.Empty;

            try
            {
                var chatCompletionResult = await chatCompletionService.GetChatMessageContentAsync(chatHistory, openAIPromptExecutionSettings, _emailReplyAssistantKernel);
                assistantReply = $"{chatCompletionResult}";
                chatHistory.AddAssistantMessage(assistantReply);
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
