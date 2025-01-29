using Microsoft.JSInterop;

namespace MailAssistant.BlazorWebApp.Helpers
{
    /// <summary>
    /// Helper class for invoking JavaScript functions from Blazor components.
    /// </summary>
    internal static class JSHelper
    {
        private static readonly ILogger _logger;

        static JSHelper()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            _logger = loggerFactory.CreateLogger("JSHelper");
        }

        /// <summary>
        /// Calls a JavaScript function asynchronously.
        /// </summary>
        /// <param name="jsRuntime">The IJSRuntime instance to use for invoking JavaScript.</param>
        /// <param name="functionName">The name of the JavaScript function to call.</param>
        /// <param name="args">The arguments to pass to the JavaScript function.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        internal static async Task CallJavaScriptFunctionAsync(IJSRuntime jsRuntime, string functionName, params object[] args)
        {
            try
            {
                await jsRuntime.InvokeVoidAsync(functionName, args);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling JavaScript function '{functionName}': {ex.Message}");
            }
        }
    }
}