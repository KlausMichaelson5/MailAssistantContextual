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
            var response =_httpClient.GetFromJsonAsync<List<Email>>(_appSettings.ApiPaths.EmailApi).Result;
            return response;
        }
    }
}
