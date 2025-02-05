using MailAssistant.BlazorWebApp.Helpers;

namespace MailAssistant.BlazorWebApp.Components.Pages
{
    /// <summary>
    /// Represents the review component for sharing email messages.
    /// </summary>
    public partial class Review
	{
        /// <summary>
        /// This will open outlook and copy the email into the body there.
        /// </summary>
        private async void ShareMessage()
        {
            await JSHelper.CallJavaScriptFunctionAsync(JS, "sendEmail", emailInfoService.Email, emailInfoService.EmailRecipient, emailInfoService.EmailSubject);
            emailInfoService.EmailReplyGenConfirmed= false;
        }
	}
}