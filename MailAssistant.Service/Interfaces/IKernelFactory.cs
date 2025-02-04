using Microsoft.SemanticKernel;

namespace MailAssistant.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for _emailReplyAssistantKernel factory.
    /// </summary>
    public interface IKernelFactory
    {
        /// <summary>
        /// Creates a new _emailReplyAssistantKernel instance configured for  OpenAI.
        /// </summary>
        /// <returns>A new instance of the <see cref="Kernel"/> class.</returns>
        Kernel GetKernel();
    }
}
