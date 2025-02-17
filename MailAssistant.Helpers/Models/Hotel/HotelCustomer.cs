namespace MailAssistant.Helpers.Models.Hotel
{
    public class HotelCustomer
    {
        public string HotelCustomerId { get; set; }= Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<HotelBooking> BookingHistory { get; set; } 
    }
}

