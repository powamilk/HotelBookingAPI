namespace HotelBookingAPI.Entities
{
    public class Phong
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public KhachSan KhachSan { get; set; }
    }
}
