using System.ComponentModel.DataAnnotations;

namespace MailAssistant.BlazorWebApp.Components.Models
{
	/// <summary>
	/// Represents the model for an email.
	/// </summary>
	public  class EmailModel
    {
		/// <summary>
		/// Gets or sets the sender's email address.
		/// </summary>
		/// <value>The sender's email address.</value>
		[Required(ErrorMessage = "Sender email id is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
		public string EmailSender { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the recipient's email address.
		/// </summary>
		/// <value>The recipient's email address.</value>
		[Required(ErrorMessage = "Recipient email id is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
		public string EmailRecipient { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the subject of the email.
		/// </summary>
		/// <value>The subject of the email.</value>
		[Required(ErrorMessage = "Subject is required")]
		public string EmailSubject { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the context of the email.
		/// </summary>
		/// <value>The context of the email.</value>
		public string EmailContext { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the received email content incase user needs assistant to draft a email response to a previous email istead, of a new email itself.
		/// </summary>
		/// <value>The received previous email content.</value>
		public string ReceivedEmail { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the purpose of the email.
		/// </summary>
		/// <value>The purpose of the email.</value>
		[Required(ErrorMessage = "Purpose is required")]
		public string EmailPurpose { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the type of the email.
		/// </summary>
		/// <value>The type of the email.</value>
		[Required(ErrorMessage = "Type of mail is required")]
		public MailType TypeOfMail { get; set; } = MailType.Professional;

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object with the values of all the parameters.</returns>
		public override string ToString()
        {
            return $"From:{EmailSender},To:{EmailRecipient},Subject:{EmailSubject},Context:{EmailContext},ReceivedEmail:{ReceivedEmail},Purpose:{EmailPurpose},Type of mail/Tone:{TypeOfMail}";
        }
    }

}
