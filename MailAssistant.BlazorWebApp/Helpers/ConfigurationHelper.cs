namespace MailAssistant.BlazorWebApp.Helpers
{
    /// <summary>
    /// Provides helper methods for Configuration management.
    /// </summary>
    internal static class ConfigurationHelper
    {
        private static readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes static members of the <see cref="ConfigurationHelper"/> class.
        /// </summary>
        static ConfigurationHelper()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
        }
        /// <summary>
        /// Gets the application Configuration.
        /// </summary>
        internal static IConfiguration Configuration => _configuration;
    }
}
