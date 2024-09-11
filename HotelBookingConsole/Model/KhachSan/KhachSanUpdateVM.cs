using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingConsole.Model.KhachSan
{
    public class KhachSanUpdateVM
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public int TotalRoom { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.Now;

    }
}
