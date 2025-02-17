using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using MailAssistant.Services.Models.AppSettingsModels;
using Microsoft.Extensions.Configuration;

namespace MailAssistant.Services.Helpers
{
    public class WebSearchPlugin
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public WebSearchPlugin(IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }
        #pragma warning disable SKEXP0050
        public  WebSearchEnginePlugin GetAzureBingSearchPlugin()
        {
            //var client = new SecretClient(new Uri(_appSettings.AzureKeyVault.BaseUrl), new DefaultAzureCredential());
            //KeyVaultSecret retrievedSecret = client.GetSecret("AzureBingSearch--ApiKey");
            var apikey = _configuration["BingApiKey"];
            var bingConnector = new BingConnector(apikey);
            var plugin = new WebSearchEnginePlugin(bingConnector);
            return plugin;
        }
        #pragma warning restore SKEXP0050
    }
}
