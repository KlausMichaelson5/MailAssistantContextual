using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace MailAssistant.Services.Helpers
{
    public static class WebSearchPlugin
    {
        #pragma warning disable SKEXP0050
        public static WebSearchEnginePlugin GetAzureBingSearchPlugin()
        {
            var configuration = ConfigurationHelper.Configuration;
            var client = new SecretClient(new Uri(configuration["AzureKeyVault:BaseUrl"]), new DefaultAzureCredential());
            KeyVaultSecret retrievedSecret = client.GetSecret("AzureBingSearch--ApiKey");
            var apikey = retrievedSecret.Value;
            var bingConnector = new BingConnector(apikey);
            var plugin = new WebSearchEnginePlugin(bingConnector);
            return plugin;


        }
        #pragma warning restore SKEXP0050
    }
}
