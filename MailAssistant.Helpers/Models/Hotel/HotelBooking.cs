namespace MailAssistant.Helpers.Models.Hotel
{
    public class HotelBooking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<Room> RoomsBooked { get; set; }
    }
}
