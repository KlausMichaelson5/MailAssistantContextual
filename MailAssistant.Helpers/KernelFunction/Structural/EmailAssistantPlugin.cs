using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MailAssistant.Helpers.KernelFunction
{
    /// <summary>
    /// Provides functions for interacting with the user and drafting emails.
    /// </summary>
    public class EmailAssistantPlugin
    {
        /// <summary>
        /// Prompts the user with options when they first enter the chatbot.
        /// </summary>
        /// <returns>A string with the options for the user to choose from.</returns>
        [KernelFunction("prompt_user")]
        [Description("Prompts the user with options when they first enter the chatbot or when the greet the chatbot or ask chatbot it's function..")]
        public async Task<string> PromptUser()
        {
            string options = "Welcome! How can I assist you today?\n" +
                             "1) Email-related grammar questions\n" +
                             "2) Request sample generic emails for various situations\n" +
                             "3) Generate an email based on provided data\n" +
                             "4) Modify an existing email";
            return await Task.FromResult(options);
        }

        /// <summary>
        /// Handles the user's choice and directs them to the appropriate function.
        /// </summary>
        /// <param name="choice">The user's choice.</param>
        /// <returns>A string response based on the user's choice.</returns>
        [KernelFunction("handle_choice")]
        [Description("Handles the user's choice and directs them to the appropriate function.")]
        public async Task<string> HandleChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    return await Task.FromResult("Please ask your email-related grammar question.");
                case 2:
                    return await Task.FromResult("Please specify the type of email you need a sample for.");
                case 3:
                    return await Task.FromResult("Please provide the data for the email you want to generate.");
                case 4:
                    return await Task.FromResult("Please provide the existing email you want to modify.");
                default:
                    return await Task.FromResult("Invalid choice. Please select a valid option.");
            }
        }
    }
}