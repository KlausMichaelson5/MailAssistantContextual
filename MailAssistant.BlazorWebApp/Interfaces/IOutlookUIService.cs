using MailAssistant.BlazorWebApp.Components.Models;

namespace MailAssistant.BlazorWebApp.Interfaces
{
    public interface IOutlookUIService
    {
        Task<List<Email>> GetEmails();
    }
}
