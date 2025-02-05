namespace MailAssistant.AzureAISearch.Models.AppSettingsModels
{
    public class AppSettings
    {
        public AzureKeyVault AzureKeyVault { get; set; }
        public AzureOpenAITextEmbedding AzureOpenAITextEmbedding { get; set; }
        public AzureAISearch AzureAISearch { get; set; }
    }
}