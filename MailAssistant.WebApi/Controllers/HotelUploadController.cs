using MailAssistant.AzureAISearch.Services;
using MailAssistant.Helpers.Models.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace MailAssistant.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelUploadController : ControllerBase
    {
        private readonly AzureAIUploadForHotelCustomerRecords _customerRecords;
        public HotelUploadController(AzureAIUploadForHotelCustomerRecords customerRecords)
        {
            _customerRecords = customerRecords;
        }
        [HttpPost("AddHotelCustomer")]
        public async Task<IActionResult> UploadHotelCustomer([FromBody] HotelCustomer customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid request.");
            }

            try
            {
                await _customerRecords.UploadData(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: Please try again later");
            }
        }
    }
}
