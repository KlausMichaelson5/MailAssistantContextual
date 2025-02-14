using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Data;

namespace MailAssistant.Helpers.Models.Hotel
{
    public class HotelCustomerVector
    {
        [VectorStoreRecordKey]
        public string HotelCustomerId { get; set; }

        [VectorStoreRecordData]
        public string FirstName { get; set; }

        [VectorStoreRecordData]
        public string LastName { get; set; }

        [VectorStoreRecordData]
        [EmailAddress]
        public string Email { get; set; }

        [VectorStoreRecordData]
        #pragma warning disable SKEXP0001
        [TextSearchResultValue]
        #pragma warning restore SKEXP0001
        public List<string> BookingHistory { get; set; } //getting from serializing BookingHistory class/by using ToString() on it.

        [VectorStoreRecordVector(Dimensions: 1536)]
        public ReadOnlyMemory<float>? BookingHistoryEmbedding { get; set; }
    }
}
