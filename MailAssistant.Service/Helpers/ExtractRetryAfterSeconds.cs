namespace MailAssistant.Services.Helpers
{
	/// <summary>
	/// Provides helper methods for extracting information from messages.
	/// </summary>
	internal static class ExtractMessageHelper
	{
		/// <summary>
		/// Extracts the "retry-after X seconds" from a given message This is used when a exception accurs because of openai/other assistant token limit is reached and we get a exception and from that message we extract the waiting time to display the same to end user..
		/// </summary>
		/// <param name="message">The message containing the retry-after information.</param>
		/// <returns>
		/// A string representing the "retry-after X seconds", or "retry later" if the information is not found.
		/// </returns>
		internal static string ExtractRetryAfterSeconds(string message)
		{
			// Example message: "Requests... retry after 34 seconds. Please retry after 45 seconds"  
			var match = System.Text.RegularExpressions.Regex.Match(message, @"retry after\s+(\d+)\s+seconds");
			if (!match.Success) { return "retry later"; }
			return $"{match}";
		}
	}
}
