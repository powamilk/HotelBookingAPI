using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingConsole.UI
{
    internal class HttpClientSingleton
    {
        private static readonly HttpClient _instance = new HttpClient();

        static HttpClientSingleton()
        {
            _instance.BaseAddress = new Uri("https://localhost:44351/api/");
        }

        public static HttpClient Instance => _instance;
    }
}
