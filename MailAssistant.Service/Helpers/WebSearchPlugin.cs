using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MailAssistant.Services.Models.AppSettingsModels;

namespace MailAssistant.Services.Helpers
{
    public class WebSearchPlugin
    {
        private readonly AppSettings _appSettings;

        public WebSearchPlugin(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        #pragma warning disable SKEXP0050
        public  WebSearchEnginePlugin GetAzureBingSearchPlugin()
        {
            var client = new SecretClient(new Uri(_appSettings.AzureKeyVault.BaseUrl), new DefaultAzureCredential());
            KeyVaultSecret retrievedSecret = client.GetSecret("AzureBingSearch--ApiKey");
            var apikey = retrievedSecret.Value;
            var bingConnector = new BingConnector(apikey);
            var plugin = new WebSearchEnginePlugin(bingConnector);
            return plugin;
        }
        #pragma warning restore SKEXP0050
    }
}
