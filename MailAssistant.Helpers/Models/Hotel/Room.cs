namespace MailAssistant.Helpers.Models.Hotel
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
