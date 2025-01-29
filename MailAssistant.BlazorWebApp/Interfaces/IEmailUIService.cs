using MailAssistant.BlazorWebApp.Components.Models;

namespace MailAssistant.BlazorWebApp.Interfaces
{
    /// <summary>
    /// Interface for email service operations.UI service uses this contract to get drated email from web api.
    /// </summary>
    internal interface IEmailUIService
    {
        /// <summary>
        /// Gets a drafted email as a response from the assistant.
        /// </summary>
        /// <param name="userRequestEmail">The user's request to draft email.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the drafted email as response from the assistant.</returns>
        Task<string> GetAssistantDraftEmail(EmailModel userRequestEmail);
    }
}