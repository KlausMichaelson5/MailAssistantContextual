using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MailAssistant.Contracts.Interfaces
{
	/// <summary>
	/// Defines the contract for a settings factory.
	/// </summary>
	public interface ISettingsFactory
    {
		/// <summary>
		/// Creates a new instance of <see cref="OpenAIPromptExecutionSettings"/>.
		/// </summary>
		/// <returns>A new instance of <see cref="OpenAIPromptExecutionSettings"/>.</returns>
		OpenAIPromptExecutionSettings CreateSettings();
    }
}
