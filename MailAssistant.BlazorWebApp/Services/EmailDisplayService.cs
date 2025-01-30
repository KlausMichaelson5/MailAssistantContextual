using MailAssistant.BlazorWebApp.Components.Models;
using MailAssistant.BlazorWebApp.Interfaces;

namespace MailAssistant.BlazorWebApp.Services
{
    public class EmailDisplayService : IEmailDisplayService
    {
        public List<Email> GetEmails()
        {
            // Sample emails
            return new List<Email>
            {
                new Email { Subject = "Welcome!", Body = "Welcome to our service." },
                new Email { Subject = "Meeting Reminder", Body = "Don't forget about the meeting tomorrow." }
            };
        }
    }
}
