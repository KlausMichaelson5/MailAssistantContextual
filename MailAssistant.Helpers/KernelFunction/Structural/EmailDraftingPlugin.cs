﻿using Microsoft.Office.Interop.Outlook;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MailAssistant.Helpers.KernelFunction
{
    /// <summary>
    /// Provides functions for drafting different parts of an email.
    /// </summary>
    public class EmailDraftingPlugin
    {
        private string currentUserName = string.Empty;
        public EmailDraftingPlugin()
        {
            Application outlookApplication = new Application();
            NameSpace outlookNamespace = outlookApplication.GetNamespace("MAPI");
            Recipient currentUser = outlookNamespace.CurrentUser;
            currentUserName = currentUser.Name; 
        }

        [KernelFunction("enforce_email_etiquette")]
        [Description("Compulsorily to be used when geenrating email")]
        public  string EnforceEmailEtiquette(string tone)
        {
            var etiquetteRules = new Dictionary<string, (string Salutation, string SignOff, List<string> PolitePhrases)>
            {
                ["Professional"] = ("Dear [Recipient],", $"Best regards,\n{currentUserName}", new List<string> { "Please", "Thank you", "Kindly" }),
                ["Casual"] = ("Hi [Recipient],", $"Cheers,\n{currentUserName}", new List<string> { "Thanks", "Cheers", "Take care" }),
                ["Friendly"] = ("Hey [Recipient],", $"Best,\n{currentUserName}", new List<string> { "Thanks a lot", "Catch you later", "Best wishes" })
            };

            return etiquetteRules.ContainsKey(tone) ? etiquetteRules[tone].ToString() : etiquetteRules["Proffesional"].ToString();
        }

        /// <summary>
        /// Generates a closing for the email based on the sender's name.
        /// </summary>
        /// <param name="senderName">The name of the email sender.</param>
        /// <param name="senderTitle">The title of the email sender.</param>
        /// <returns>A closing string.</returns>
        [KernelFunction("generate_closing")]
        [Description("Generates a closing for the email.")]
        public async Task<string> GenerateClosing()
        {
            return await Task.FromResult($"Best regards,\n{currentUserName}");
        }
    }
}