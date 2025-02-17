using System.Text.RegularExpressions;
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
            emailInfoService.EmailSender = selectedEmail.From;
            emailInfoService.EmailSubject = selectedEmail.Subject;
            emailInfoService.EmailReplyGenConfirmed = true;
            Navigation.NavigateTo("/Chatbot");

        }
        private static string GetNameFromEmail(string email)
        {
            Match match = Regex.Match(email, @"([^@]+)@");
            if (match.Success)
            {
                string name = match.Groups[1].Value.Replace('.', ' ').Replace('_', ' ');
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            }
            return "NA";
        }
    }
}