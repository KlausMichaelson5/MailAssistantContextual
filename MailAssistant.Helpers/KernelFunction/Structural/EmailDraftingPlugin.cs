using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MailAssistant.Helpers.KernelFunction
{
    /// <summary>
    /// Provides functions for drafting different parts of an email.
    /// </summary>
    public class EmailDraftingPlugin
    {
        /// <summary>
        /// Generates the body of the email based on the provided context and purpose.
        /// </summary>
        /// <param name="context">The context of the email.</param>
        /// <param name="purpose">The purpose of the email.</param>
        /// <returns>The body of the email.</returns>
        [KernelFunction("generate_body")]
        [Description("Generates the body of the email based on the provided context and purpose.After greeting this will be added")]
        public async Task<string> GenerateBody(string context, string purpose)
        {
            return await Task.FromResult($"I hope this message finds you well. I am writing to {purpose}. {context}");
        }

        /// <summary>
        /// Generates a closing for the email based on the sender's name.
        /// </summary>
        /// <param name="senderName">The name of the email sender.</param>
        /// <param name="senderTitle">The title of the email sender.</param>
        /// <returns>A closing string.</returns>
        [KernelFunction("generate_closing")]
        [Description("Generates a closing for the email based on the sender's name.After body this will be added")]
        public async Task<string> GenerateClosing(string senderName)
        {
            return await Task.FromResult($"Best regards,\n{senderName}");
        }
    }
}