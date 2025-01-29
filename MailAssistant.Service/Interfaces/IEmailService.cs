namespace MailAssistant.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for an email service.Used by a service to get the assistant drafted email.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Gets a draft email from the assistant based on the user's request.
        /// </summary>
        /// <param name="userRequest">The user's request for the email draft.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the drafted email by assistant based on user request.</returns>
        Task<string> GetAssistantDraftEmail(string userRequest);
    }
}
