using MailAssistant.WebApi.Interfaces;
using MailAssistant.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailAssistant.WebAPI.Controllers
{
    /// <summary>
    /// API controller for handling email-related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailDataService _emailDataService;
        private readonly ILogger<EmailController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailController"/> class.
        /// </summary>
        /// <param name="emailDataService">The email data service.</param>
        /// <param name="logger">The Logger client to log errors and information.</param>
        public EmailController(IEmailDataService emailDataService, ILogger<EmailController> logger)
        {
            _emailDataService = emailDataService;
            _logger = logger;   
        }

        /// <summary>
        /// Gets a draft email based on the user's userRequestEmail.
        /// </summary>
        /// <param name="userRequestEmail">The email userRequestEmail containing the user's userRequestEmail.</param>
        /// <returns>An <see cref="IActionResult"/> containing the draft email.</returns>
        [HttpPost("GetDraftEmail")]
        public async Task<IActionResult> GetDraftEmail([FromBody] EmailModel userRequestEmail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validationErrors = ModelState.Values.SelectMany(v => v.Errors.Select(m=>m.ErrorMessage));
                    _logger.LogWarning("Validation error: {Errors}", validationErrors);
                    return BadRequest(validationErrors);
                }

                var response = await _emailDataService.GetDraftEmail(userRequestEmail);
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