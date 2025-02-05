using MailAssistant.BlazorWebApp.Components.Models;
using MailAssistant.BlazorWebApp.Helpers;
using Microsoft.AspNetCore.Components.Web;

namespace MailAssistant.BlazorWebApp.Components.Pages
{
    /// <summary>
    /// Represents the chatbot component for handling user interactions and messages.
    /// </summary>
    public partial class Chatbot
	{
		private string userRequest=string.Empty;
		private List<Message> messages = new List<Message>();
		private bool isLoading = false;
		private bool newChatSession = true;


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (emailInfoService!=null && emailInfoService.EmailReplyGenConfirmed==true)
                {
                    messages.Add(new Message { Text = emailInfoService.Email, MessageType = MessageType.User });
                    StateHasChanged();
                    await JSHelper.CallJavaScriptFunctionAsync(JS, "scrollToBottom");
                    var emailReplyGenPrompt = File.ReadAllText(appSettings.Value.Prompts.EmailReplyGenerator);
                    await GetAssistantReply($"{emailReplyGenPrompt}{emailInfoService.Email}");
                    StateHasChanged();
                    await JSHelper.CallJavaScriptFunctionAsync(JS, "scrollToBottom");
                    emailInfoService.EmailReplyGenConfirmed = false;
                }
            }
        }

        /// <summary>
        /// Sends the user's request and also adds it to the message list.
        /// </summary>
        private async Task SendMessage()
		{

			if (!string.IsNullOrWhiteSpace(userRequest))
			{
				
				messages.Add(new Message { Text = this.userRequest, MessageType = MessageType.User });
                StateHasChanged();

				await JSHelper.CallJavaScriptFunctionAsync(JS,"disableButtons");
				var userRequest = this.userRequest;
				this.userRequest = string.Empty;
				await GetAssistantReply(userRequest);
                await JSHelper.CallJavaScriptFunctionAsync(JS, "scrollToBottom");
                await JSHelper.CallJavaScriptFunctionAsync(JS, "enableButtons");
            }
        }

        /// <summary>
        /// Gets the assistant's reply to the user's message.
        /// </summary>
        /// <param name="userRequest">The user's message.</param>
        private async Task GetAssistantReply(string userRequest)
		{
			isLoading = true;
			StateHasChanged();
            await JSHelper.CallJavaScriptFunctionAsync(JS, "scrollToBottom");

            var assistantReply = await chatService.GetChatAssistantResponse(userRequest, newChatSession);
			newChatSession = false;

			if (assistantReply != null)
			{
				messages.Add(new Message { Text = assistantReply, MessageType = MessageType.Bot });
			}
            isLoading = false;
            StateHasChanged();
        }

        /// <summary>
        /// Handles the key down event for sending messages.Used to send message when user clicked enter.
        /// </summary>
        /// <param name="e">The keyboard event arguments.</param>
        private async void HandleKeyDown(KeyboardEventArgs e)
		{
			if (e.Key == "Enter" && !e.ShiftKey)
			{
				await SendMessage();
			}
		}

        /// <summary>
        /// Edits an existing message.
        /// </summary>
        /// <param name="message">The message to edit.</param>
        private void EditMessage(Message message)
		{
			userRequest = message.Text;

        }

		/// <summary>
		/// Copies a message to the clipboard.
		/// </summary>
		/// <param name="message">The message to copy.</param>
		private async void CopyMessage(Message message)
		{
            await JSHelper.CallJavaScriptFunctionAsync(JS, "copyToClipboard", message.Text);
		}

        /// <summary>
        /// Lets user review the drafted mail by navigating to the review page and copying it there to be able to edit and share.
        /// </summary>
        /// <param name="message">The message to review.</param>
        private void ReviewMessage(Message message)
		{
            emailInfoService.Email = message.Text;
            Navigation.NavigateTo("/review");
		}

        /// <summary>
        /// Shares a message via email.This will only open outlook and copy the email into the body there.
        /// </summary>
        /// <param name="message">The message to share.</param>
        private async void ShareMessage(Message message)
		{
            emailInfoService.Email = message.Text;
            await JSHelper.CallJavaScriptFunctionAsync(JS, "sendEmail", emailInfoService.Email, emailInfoService.EmailRecipient, emailInfoService.EmailSubject);
        }

		/// <summary>
		/// Clears all messages and starts a new chat session.
		/// </summary>
		private void ClearMessages()
		{
			messages.Clear();
			newChatSession = true;
		}
	}
}