namespace MailAssistant.Services.Models.AppSettingsModels
{
    public class AppSettings
    {
        public AzureKeyVault AzureKeyVault { get; set; }
        public AzureOpenAI AzureOpenAI { get; set; }
        public SystemChatMessages SystemChatMessages { get; set; }
        public Outlook Outlook { get; set; }
    }
}