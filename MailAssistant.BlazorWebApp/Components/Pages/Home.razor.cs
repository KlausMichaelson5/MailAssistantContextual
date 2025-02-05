using MailAssistant.BlazorWebApp.Components.Models;

namespace MailAssistant.BlazorWebApp.Components.Pages
{
    public partial class Home
    {
        private bool isLoading = true;
        private List<Email> emails = [];
        private Email selectedEmail;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isLoading = true;
                StateHasChanged();
                emails = await emailDisplayService.GetEmails();
                isLoading = false;
                StateHasChanged();
            }
        }

        private void SelectEmail(Email email)
        {
            selectedEmail = email;
        }

        private void GenerateAIReply()
        {
            emailInfoService.Email = selectedEmail.Body;
            emailInfoService.EmailRecipient = selectedEmail.From;
            emailInfoService.EmailSubject = selectedEmail.Subject;
            emailInfoService.EmailReplyGenConfirmed = true;
            Navigation.NavigateTo("/Chatbot");

        }
    }
}