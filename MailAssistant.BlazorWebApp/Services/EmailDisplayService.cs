using MailAssistant.BlazorWebApp.Components.Models;
using MailAssistant.BlazorWebApp.Interfaces;
using Microsoft.Extensions.Options;

namespace MailAssistant.BlazorWebApp.Services
{
    public class EmailDisplayService : IEmailDisplayService
    {

        private readonly HttpClient _httpClient;
        private readonly Models.AppSettings _appSettings;
        private readonly ILogger<EmailDisplayService> _logger;

        public EmailDisplayService(HttpClient httpClient, IOptions<Models.AppSettings> appSettings, ILogger<EmailDisplayService> logger)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task<List<Email>> GetEmails()
        {
            List<Email> response = [];
            try
            {
                var httpResponse = await _httpClient.GetAsync(_appSettings.ApiPaths.EmailApi);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    _logger.LogError($"Internal error status code: {httpResponse.StatusCode} response: {httpResponse}");
                    response=[];
                }
                else
                {
                    var emails = await httpResponse.Content.ReadFromJsonAsync<List<Email>>();
                    if(emails != null ) { response=emails; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Internal server error. Please try again later");
            }
            return response;
        }
    }
}
