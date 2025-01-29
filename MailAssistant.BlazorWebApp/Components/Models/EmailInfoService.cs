namespace MailAssistant.BlazorWebApp.Components.Models
{
    /// <summary>
    /// Provides information about an email.Used to pass email between home/chatbot page and to review page.
    /// </summary>
    internal class EmailInfoService
    {
        /// <summary>
        /// Gets or sets the email recipient.
        /// </summary>
        /// <value>The email recipient.</value>
        internal string EmailRecipient { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        /// <value>The email content.</value>
        internal string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        /// <value>The email subject.</value>
        internal string EmailSubject { get; set; } =string.Empty;
	}
}
