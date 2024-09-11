using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HotelBookingConsole.Model.DatPhong;
using Newtonsoft.Json;

namespace HotelBookingConsole.UI
{
    internal class DatPhongUIService
    {
        private readonly string baseUrl = "https://localhost:44351/api/";

        public async Task GetAll()
        {
            string urlGet = $"{baseUrl}bookings";
            HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(urlGet);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
                return;
            }

            string data = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<List<DatPhongVM>>(data);

            foreach (var booking in bookings)
            {
                Console.WriteLine($"Khách hàng: {booking.CustomerId} - Phòng: {booking.RoomId} - Ngày nhận: {booking.CheckInDate}");
            }
        }

        public async Task Post(DatPhongCreateVM vm)
        {
            string urlPost = $"{baseUrl}bookings";
            string jsonRequest = JsonConvert.SerializeObject(vm);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PostAsync(urlPost, content);

            Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
        }
        public async Task Put(int id, DatPhongUpdateVM vm)
        {
            string urlPut = $"{baseUrl}bookings/{id}/status";
            string jsonRequest = JsonConvert.SerializeObject(vm);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PutAsync(urlPut, content);

            Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
        }
        public async Task Delete(int id)
        {
            string urlDelete = $"{baseUrl}bookings/{id}";
            HttpResponseMessage response = await HttpClientSingleton.Instance.DeleteAsync(urlDelete);

            Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
        }
    }
}
