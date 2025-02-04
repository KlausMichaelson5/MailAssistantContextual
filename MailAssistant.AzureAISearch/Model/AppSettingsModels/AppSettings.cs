namespace MailAssistant.AzureAISearch.Model.AppSettingsModels
{
    public class AppSettings
    {
        public AzureKeyVault AzureKeyVault { get; set; }
        public AzureOpenAI AzureOpenAI { get; set; }
        public AzureOpenAITextEmbedding AzureOpenAITextEmbedding { get; set; }
        public AzureAISearch AzureAISearch { get; set; }
        public SystemChatMessages SystemChatMessages { get; set; }
    }
}