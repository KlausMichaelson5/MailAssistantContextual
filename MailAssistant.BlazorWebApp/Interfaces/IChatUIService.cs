namespace MailAssistant.BlazorWebApp.Interfaces
{
    /// <summary>
    /// Interface for chat service operations.UI service uses this contract to get chat response from webapi.
    /// </summary>
    internal interface IChatUIService
    {
        /// <summary>
        /// Gets the response from the chat assistant.
        /// </summary>
        /// <param name="inputMessage">The input message from the user.</param>
        /// <param name="newChat">Indicates whether this is a new chat session.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the chat assistant's response.</returns>
         Task<string> GetChatAssistantResponse(string inputMessage, bool newChat);
    }
}