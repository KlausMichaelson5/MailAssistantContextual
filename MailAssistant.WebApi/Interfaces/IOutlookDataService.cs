using MailAssistant.Helpers.Models;

namespace MailAssistant.WebApi.Interfaces
{
    public interface IOutlookDataService
    {
        Task<List<Email>> GenerateEmbeddingsAndSearchAsync(string query, int top, int skip);
        Task GenerateEmbeddingsAndUpsertAsync(int count);
        Task<List<Email>> GetMailsFromOutlook(int count);
    }
}