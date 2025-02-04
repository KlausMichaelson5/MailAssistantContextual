using MailAssistant.BlazorWebApp.Components.Models;

namespace MailAssistant.BlazorWebApp.Interfaces
{
    public interface IEmailDisplayService
    {
        Task<List<Email>> GetEmails();
    }
}
