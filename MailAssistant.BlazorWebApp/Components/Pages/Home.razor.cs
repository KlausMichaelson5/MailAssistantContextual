using MailAssistant.BlazorWebApp.Components.Models;
using MailAssistant.BlazorWebApp.Helpers;

namespace MailAssistant.BlazorWebApp.Components.Pages
{
    /// <summary>
    /// Represents the home component for generating email.
    /// </summary>
    public partial class Home
	{

		private bool isEmailReply = false;
		private bool isLoading = false;
		private EmailModel emailModel = new EmailModel();
		private string message = string.Empty;

		/// <summary>
		/// Get the email data from model and call the assistant function to draft email.
		/// </summary>
		private async void GenerateEmail()
		{
            await JSHelper.CallJavaScriptFunctionAsync(JS, "disableButtons");
            await JSHelper.CallJavaScriptFunctionAsync(JS, "scrollWindowToBottom");
            var userRequestEmail =emailModel;
			await GetAssistantReply(userRequestEmail);
            await JSHelper.CallJavaScriptFunctionAsync(JS, "enableButtons");
            await JSHelper.CallJavaScriptFunctionAsync(JS, "scrollWindowToBottom");
        }

        /// <summary>
        /// Gets the assistant's reply to the user's email request.
        /// </summary>
        /// <param name="userRequest">The user's email request.</param>
        private async Task GetAssistantReply(EmailModel userRequestEmail)
		{
			isLoading = true;
			StateHasChanged();

			var assistantReply = await emailService.GetAssistantDraftEmail(userRequestEmail);
			if (assistantReply != null)
			{
				message = assistantReply;
			}

			isLoading = false;
			StateHasChanged();
		}


		/// <summary>
		/// Clears the email form.
		/// </summary>
		private async  void ClearForm()
		{
			emailModel = new EmailModel();
            message = string.Empty;
            await JSHelper.CallJavaScriptFunctionAsync(JS, "scrollWindowToTop");
        }

        /// <summary>
        /// Copies the specified message to the clipboard.
        /// </summary>
        /// <param name="message">The message to copy.</param>
        private async void CopyMessage(string message)
		{
            await JSHelper.CallJavaScriptFunctionAsync(JS, "copyToClipboard", message);
		}

        /// <summary>
        /// Lets user review the drafted mail by navigating to the review page and copying it there to be able to edit and share.
        /// </summary>
        /// <param name="message">The message to review.</param>
        private void ReviewMessage(string message)
		{
			emailToReview.EmailRecipient =emailModel.EmailRecipient;
			emailToReview.EmailSubject =emailModel.EmailSubject;
			emailToReview.Email= message;
			Navigation.NavigateTo("/review");
		}

        /// <summary>
        /// Shares the email via default email app.This will only open outlook/chrome in windows and copy the email into the body there.
        /// </summary>
        /// <param name="message">The message to share.</param>
        private async void ShareMessage(string message)
		{
            await JSHelper.CallJavaScriptFunctionAsync(JS, "sendEmail", message, emailModel.EmailRecipient, emailModel.EmailSubject);
		}

        /// <summary>
        /// Takes this email to chatbot.For further optimisations.
        /// </summary>
        /// <param name="message">The message to share.</param>
        private async void AskChatbot(string message)
        {
			emailToOptimize.Email = message;
			await Task.Run(()=>{ Navigation.NavigateTo("/Chatbot"); });
            await JSHelper.CallJavaScriptFunctionAsync(JS, "simulateChatbotButtonClick");
        }
    }
}