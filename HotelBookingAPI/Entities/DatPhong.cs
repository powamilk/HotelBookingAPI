namespace HotelBookingAPI.Entities
{
    public class DatPhong
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; } 
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
        public KhachSan KhachSan { get; set; }
        public KhachHang KhachHang { get; set; }

    }
}
