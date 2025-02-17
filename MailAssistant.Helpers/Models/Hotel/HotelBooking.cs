namespace MailAssistant.Helpers.Models.Hotel
{
    public class HotelBooking
    {
        public string BookingId { get; set; }= Guid.NewGuid().ToString();
        public string CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<Room> RoomsBooked { get; set; }
    }
}
