namespace MailAssistant.Helpers.Models.Hotel
{
    public class Room
    {
        public string RoomId { get; set; }= Guid.NewGuid().ToString();
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
