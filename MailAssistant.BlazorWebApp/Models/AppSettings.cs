using Microsoft.Identity.Client;

namespace MailAssistant.BlazorWebApp.Models
{
    public class AppSettings
    {
        public ApiPaths ApiPaths { get; set; }
        public Prompts Prompts { get; set; }
    }
}
