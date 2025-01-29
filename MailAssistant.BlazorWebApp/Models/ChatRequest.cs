namespace MailAssistant.BlazorWebApp.Models
{
	/// <summary>
	/// Represents a request for a chat response.
	/// </summary>
	internal class ChatRequest
    {
		/// <summary>
		/// Gets or sets the input message from the user.
		/// </summary>
		public string InputMessage { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets a value indicating whether this is a new chat session.
		/// </summary>
		public bool NewChat { get; set; }
    }
}