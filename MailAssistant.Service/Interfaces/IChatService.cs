namespace MailAssistant.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for a chat service.Used by service to get response from chat assistant.
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// Gets the response from the chat assistant.
        /// </summary>
        /// <param name="inputMessage">The input message from the user.</param>
        /// <param name="newChat">Indicates whether this is a new chat session.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the assistant's response.</returns>
        Task<string> GetChatAssistantResponse(string inputMessage, bool newChat);
    }
}
