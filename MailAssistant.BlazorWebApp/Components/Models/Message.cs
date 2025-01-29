namespace MailAssistant.BlazorWebApp.Components.Models
{
	/// <summary>
	/// Represents a message with text, message type, and sent time.Used to display chat messages between user and assistant
	/// </summary>
	internal class Message
	{
		/// <summary>
		/// Gets or sets the text of the message.
		/// </summary>
		/// <value>The text of the message.</value>
		internal string Text { get; set; }=string.Empty;

		/// <summary>
		/// Gets or sets the type of the message.Whether this a user request chat message or assistant response one.
		/// </summary>
		/// <value>The type of the message.</value>
		internal MessageType MessageType { get; set; }

		//This is currently not used.
		/// <summary>
		/// Gets or sets the time the message was sent.
		/// </summary>
		/// <value>The time the message was sent.</value>
		internal DateTime SentTime { get; set; }
	}
}
