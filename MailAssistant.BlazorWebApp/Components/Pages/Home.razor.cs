using MailAssistant.BlazorWebApp.Components.Models;

namespace MailAssistant.BlazorWebApp.Components.Pages
{
    public partial class Home
    {
        private bool isLoading = true;
        private List<Email> emails = [];
        private Email selectedEmail;

        protected async override Task OnInitializedAsync()
        {
            isLoading = true;
            StateHasChanged();
            emails =await emailDisplayService.GetEmails();
            isLoading = false;
            StateHasChanged();
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