using MailAssistant.WebApi.Interfaces;
using MailAssistant.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailAssistant.WebAPI.Controllers
{
    /// <summary>
    /// API controller for handling chat-related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatDataService _chatDataService;
        private readonly ILogger<ChatController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatController"/> class.
        /// </summary>
        /// <param name="chatDataService">The chat data service.</param>
        /// <param name="logger">The Logger client to log errors and information.</param>
        public ChatController(IChatDataService chatDataService,ILogger<ChatController> logger)
        {
            _chatDataService = chatDataService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a chat response based on the user's input message.
        /// </summary>
        /// <param name="request">The chat request containing the user's input message.</param>
        /// <returns>An <see cref="IActionResult"/> containing the chat response.</returns>
        [HttpPost("GetResponse")]
        public async Task<IActionResult> GetResponse([FromBody] ChatRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.InputMessage))
            {
                _logger.LogWarning($"Invalid request.");
                return BadRequest("Invalid request.");
            }

            try
            {
                var response = await _chatDataService.GetResponse(request.InputMessage, request.NewChat);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error:{ex.Message}");
                return StatusCode(500, $"Internal server error: Please try again later");
            }
        }
    }
}