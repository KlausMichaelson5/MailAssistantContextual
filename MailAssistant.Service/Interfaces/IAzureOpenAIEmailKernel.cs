using Microsoft.SemanticKernel;

namespace MailAssistant.Services.Interfaces
{
    public interface IAzureOpenAIEmailKernel
    {
        Kernel GetKernel();
    }
}