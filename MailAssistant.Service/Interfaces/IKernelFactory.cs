using Microsoft.SemanticKernel;

namespace MailAssistant.Contracts.Interfaces
{
	/// <summary>
	/// Defines the contract for kernel factory.
	/// </summary>
	public interface IKernelFactory
    {
		/// <summary>
		/// Creates a new kernel instance configured for  OpenAI.
		/// </summary>
		/// <returns>A new instance of the <see cref="Kernel"/> class.</returns>
		Kernel CreateKernel();
    }
}
