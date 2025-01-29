using MailAssistant.WebApi.Models;

namespace MailAssistant.WebApi.Interfaces
{
    /// <summary>
    /// Defines the methods for email data services.Used by WebAPI to interact with DLL/service to get data.
    /// </summary>
    public interface IEmailDataService
    {
        /// <summary>
        /// Gets a draft email based on the user's request.
        /// </summary>
        /// <param name="userRequestEmail">The user's input message.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the draft email.</returns>
        Task<String> GetDraftEmail(EmailModel  userRequestEmail);
    }
}