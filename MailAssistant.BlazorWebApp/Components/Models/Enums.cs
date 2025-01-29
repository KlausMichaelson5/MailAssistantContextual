namespace MailAssistant.BlazorWebApp.Components.Models
{
	/// <summary>
	/// Specifies the type of email.
	/// </summary>
	public enum MailType
    {
		/// <summary>
		/// A professional email.
		/// </summary>
		Professional,

		/// <summary>
		/// A friendly email.
		/// </summary>
		Friendly,

		/// <summary>
		/// A casual email.
		/// </summary>
		Casual
	}

	/// <summary>
	/// Specifies the type of message.
	/// </summary>
	internal enum MessageType
	{
		/// <summary>
		/// A message from the user.
		/// </summary>
		User,
		/// <summary>
		/// A message from the bot.
		/// </summary>
		Bot
	}
}