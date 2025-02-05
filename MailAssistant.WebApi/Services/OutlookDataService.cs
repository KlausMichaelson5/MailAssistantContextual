using MailAssistant.Helpers.Models;
using MailAssistant.Services.Interfaces;
using MailAssistant.WebApi.Interfaces;

namespace MailAssistant.WebApi.Services
{
    internal class OutlookDataService : IOutlookDataService
    {
        private readonly IOutlookService _outlookService;
        private readonly ILogger<OutlookDataService> _logger;

        public OutlookDataService(IOutlookService outlookService, ILogger<OutlookDataService> logger)
        {
            _outlookService = outlookService;
            _logger = logger;
        }

        public async Task<List<Email>> GetMailsFromOutlook(int count)
        {
            try
            {
                return await _outlookService.GetMailsFromOutlook(count); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching emails: {ex.Message}");
                throw;
            }
        }

        public async Task GenerateEmbeddingsAndUpsertAsync(int count)
        {
            try
            {
                await _outlookService.GenerateEmbeddingsAndUpsertAsync(count);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating embeddings: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Email>> GenerateEmbeddingsAndSearchAsync(string query, int top, int skip)
        {
            try
            {
                return await _outlookService.GenerateEmbeddingsAndSearchAsync(query, top, skip);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error searching emails: {ex.Message}");
                throw;
            }
        }
    }
}