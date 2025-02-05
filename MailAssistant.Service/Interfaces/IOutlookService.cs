using MailAssistant.Helpers.Models;

namespace MailAssistant.Services.Interfaces
{
    public interface IOutlookService
    {
        Task<List<Email>> GetMailsFromOutlook(int count = int.MaxValue);
        Task GenerateEmbeddingsAndUpsertAsync(int count = int.MaxValue);
        Task<List<Email>> GenerateEmbeddingsAndSearchAsync(string query, int top = 1, int skip = 0);
    }
}