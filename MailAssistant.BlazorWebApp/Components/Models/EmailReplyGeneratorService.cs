namespace MailAssistant.BlazorWebApp.Components.Models
{
    /// <summary>
    /// Provides information about an email.Used to pass email between home page and to chatbot page.
    /// </summary>
    internal class EmailReplyGeneratorService
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        internal string Email { get; set; } = string.Empty;
    }
}
