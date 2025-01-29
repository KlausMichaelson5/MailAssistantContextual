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
            if(!string.IsNullOrEmpty(emailToReview.EmailRecipient))
            {
                await JSHelper.CallJavaScriptFunctionAsync(JS, "sendEmail", emailToReview.Email, emailToReview.EmailRecipient, emailToReview.EmailSubject);

                emailToReview.Email=string.Empty;
                emailToReview.EmailRecipient=string.Empty;
                emailToReview.EmailSubject=string.Empty;
            }
            await JSHelper.CallJavaScriptFunctionAsync(JS, "sendEmail", emailToReview.Email);
            emailToReview.Email = string.Empty;
        }
	}
}