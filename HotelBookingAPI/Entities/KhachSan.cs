using System.Security.Cryptography.X509Certificates;
using HotelBookingAPI.Entities;

namespace HotelBookingAPI.Entities
{
    public class KhachSan
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public int TotalRoom { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public ICollection<Phong> Phongs { get; set; }
    }
}
