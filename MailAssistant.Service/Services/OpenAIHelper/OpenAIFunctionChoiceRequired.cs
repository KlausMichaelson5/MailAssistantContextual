using MailAssistant.Contracts.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MailAssistant.Services.Services
{
	/// <summary>
	/// Represents a factory for creating OpenAI prompt execution settings with  function choice behavior as required that implements ISettingsFactory.
	/// </summary>
	public class OpenAIFunctionChoiceRequired : ISettingsFactory
    {
		/// <summary>
		/// Creates a new instance of <see cref="OpenAIPromptExecutionSettings"/> with function choice behavior as required.
		/// </summary>
		/// <returns>A new instance of <see cref="OpenAIPromptExecutionSettings"/>.</returns>
		public OpenAIPromptExecutionSettings CreateSettings()
        {
			return new OpenAIPromptExecutionSettings
			{
				FunctionChoiceBehavior = FunctionChoiceBehavior.Required()
			};
        }
    }

}
