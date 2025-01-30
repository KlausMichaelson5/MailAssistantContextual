namespace MailAssistant.BlazorWebApp.Services
{
    public interface IEmailDisplayService
    {
        List<Email> GetEmails();
    }

    public class Email
    {
        public string Subject=string.Empty;
        public string Body=string.Empty;
    }

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
