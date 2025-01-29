using System.Net;
using MailAssistant.BlazorWebApp.Components.Models;
using MailAssistant.BlazorWebApp.Interfaces;
using MailAssistant.BlazorWebApp.Models;


namespace MailAssistant.BlazorWebApp.Services
{
    /// <summary>
    /// Provides email services to UI by interacting with an HTTP API.
    /// </summary>
    internal class EmailUIService : IEmailUIService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiPaths _apiPaths;
        private readonly ILogger<EmailUIService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailUIService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for requests.</param>
        /// <param name="apiPaths">The Api Paths to use for requests(Chat path or Email path).</param>
        /// <param name="logger">The Logger client to log errors and information.</param>
        public EmailUIService(HttpClient httpClient,ApiPaths apiPaths, ILogger<EmailUIService> logger)
        {
            _httpClient = httpClient;
            _apiPaths = apiPaths;
            _logger = logger;   
        }

        public async Task<string> GetAssistantDraftEmail(EmailModel userRequestEmail)
        {
            var response=string.Empty;
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync(_apiPaths.EmailApi, userRequestEmail);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var badRequestErrors = await httpResponse.Content.ReadAsStringAsync();
                        response = $"Validation errors: {badRequestErrors.Trim('[', ']').Replace('"', ' ')}";

                    }
                    else
                    {
                         response = $"{httpResponse.ReasonPhrase}.Please try again later";
                    }
                    _logger.LogWarning($"Internal error status code:{httpResponse.StatusCode} response:{response} ");

                }
                else
                {
                    response = await httpResponse.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = "Internal server error.Please try again later";
            }
            return response;
        }
    }
}