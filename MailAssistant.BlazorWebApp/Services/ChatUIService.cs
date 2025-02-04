using MailAssistant.BlazorWebApp.Interfaces;
using MailAssistant.BlazorWebApp.Models;
using Microsoft.Extensions.Options;

namespace MailAssistant.BlazorWebApp.Services
{
    /// <summary>
    /// Provides chat services to UI by interacting with an HTTP API.
    /// </summary>
    internal class ChatUIService : IChatUIService
    {
        private readonly HttpClient _httpClient;
        private readonly Models.AppSettings _appSettings;
        private readonly ILogger<ChatUIService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatUIService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for requests.</param>
        /// <param name="apiPaths">The Api Paths to use for requests(Chat path or Email path).</param>
        /// <param name="logger">The Logger client to log errors and information.</param>
        public ChatUIService(HttpClient httpClient, IOptions<Models.AppSettings> appSettings, ILogger<ChatUIService> logger)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
            _logger = logger;   
        }
        public async Task<string> GetChatAssistantResponse(string inputMessage, bool newChat)
        {
            var response=string.Empty;
            var request = new ChatRequest
            {
                InputMessage = inputMessage,
                NewChat = newChat
            };
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync(_appSettings.ApiPaths.ChatApi, request);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    _logger.LogError($"Internal error status code:{httpResponse.StatusCode} response:{httpResponse} ");
                    response = $"{httpResponse.ReasonPhrase}.Please try again later";
                }
                else
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                response= "Internal server error.Please try again later";
            }
            return response;
        }
    }

}