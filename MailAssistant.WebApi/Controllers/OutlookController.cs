using MailAssistant.Helpers.Models;
using MailAssistant.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MailAssistant.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OutlookController : ControllerBase
    {
        private readonly IOutlookDataService _outlookDataService;
        private readonly ILogger<OutlookController> _logger;

        public OutlookController(IOutlookDataService outlookDataService, ILogger<OutlookController> logger)
        {
            _outlookDataService = outlookDataService;
            _logger = logger;
        }

        [HttpGet("Emails")]
        public async Task<IActionResult> GetMailsFromOutlook(int count = int.MaxValue)
        {
            try
            {
                var emails = await _outlookDataService.GetMailsFromOutlook(count);
                return Ok(emails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching emails: {ex.Message}");
                return StatusCode(500, "Internal server error: Please try again later");
            }
        }

        [HttpPost("Generate-Embeddings")]
        public async Task<IActionResult> GenerateEmbeddingsAndUpsertAsync(int count = int.MaxValue)
        {
            try
            {
                await _outlookDataService.GenerateEmbeddingsAndUpsertAsync(count);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating embeddings: {ex.Message}");
                return StatusCode(500, "Internal server error: Please try again later");
            }
        }

        [HttpGet("Search-Embeddings")]
        public async Task<ActionResult<List<Email>>> SearchEmails(string query, int top = 1, int skip = 0)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be empty.");
            }

            try
            {
                var emails = await _outlookDataService.GenerateEmbeddingsAndSearchAsync(query, top, skip);
                return Ok(emails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error searching emails: {ex.Message}");
                return StatusCode(500, "Internal server error: Please try again later");
            }
        }
    }
}