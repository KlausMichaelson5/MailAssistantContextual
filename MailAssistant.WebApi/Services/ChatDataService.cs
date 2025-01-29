using MailAssistant.Services.Interfaces;
using MailAssistant.WebApi.Interfaces;

namespace MailAssistant.WebApi.Services
{
    /// <summary>
    /// Provides chat data service extending <see cref="IChatDataService"/>.
    /// </summary>
    internal class ChatDataService:IChatDataService
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatDataService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatDataService"/> class.
        /// </summary>
        /// <param name="chatService">The chat service.</param>
        /// <param name="logger">The Logger client to log errors and information.</param>
        public ChatDataService(IChatService chatService,ILogger<ChatDataService> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        public async Task<string> GetResponse(string inputMessage, bool newChat)
        {
            var response=string.Empty;
            try
            {
                response= await _chatService.GetChatAssistantResponse(inputMessage, newChat);
            }
            catch (Exception ex)
            {
                response = "Internal server error.Please try again later.";
                _logger.LogError(ex.Message);      
            }
            return response;
        }
    }
}