using MailAssistant.BlazorWebApp.Components.Models;
using MailAssistant.BlazorWebApp.Interfaces;
using Microsoft.AspNetCore.Components;

namespace MailAssistant.BlazorWebApp.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public IEmailDisplayService EmailService { get; set; }

        private List<Email> emails;
        private Email selectedEmail;

        protected async override Task OnInitializedAsync()
        {
            emails =await EmailService.GetEmails();
        }

        private void SelectEmail(Email email)
        {
            selectedEmail = email;
        }

        private void GenerateAIReply()
        {
            // Placeholder for AI reply generation logic
            // This could be integrated with an AI service to generate a reply
            emailToGenerateReply.Email = $"Subject:{selectedEmail.Subject} Body:{selectedEmail.Body}";
            Navigation.NavigateTo("/Chatbot");

        }
    }
}