namespace MailAssistant.WebApi.Interfaces
{
    /// <summary>
    /// Defines the methods for chat data services.Used by WebAPI to interact with DLL/service to get data.
    /// </summary>
    public interface IChatDataService
    {
        /// <summary>
        /// Gets a response based on the user's request.
        /// </summary>
        /// <param name="userRequest">The user's input message.</param>
        /// <param name="newChat">Indicates whether this is a new chat session.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the chat response.</returns>
        Task<string> GetResponse(string userRequest, bool newChat);
    }
}