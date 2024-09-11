using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HotelBookingConsole.Model.Phong;
using Newtonsoft.Json;

namespace HotelBookingConsole.UI
{
    internal class PhongUIService
    {
        private readonly string baseUrl = "https://localhost:44351/api/hotels/";
        public async Task GetAllRoomsByHotel(int hotelId)
        {
            string url = $"{baseUrl}{hotelId}/rooms";
            HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                return;
            }

            string data = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<List<PhongVM>>(data);

            Console.WriteLine("Danh sách phòng:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"ID: {room.Id}, Loại phòng: {room.RoomType}, Giá: {room.Price}, Trạng thái: {room.Status}, Check-in: {room.CheckInTime}, Check-out: {room.CheckOutTime}");
            }
        }

        public async Task CreateRoom(int hotelId, PhongCreateVM room)
        {
            string url = $"{baseUrl}{hotelId}/rooms";
            room.Status = room.Status.Trim().ToLower() == "còn trống" ? "con trong" : "da dat";

            string jsonRequest = JsonConvert.SerializeObject(room);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                string errorDetail = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Chi tiết lỗi: {errorDetail}");
                return;
            }

            Console.WriteLine("Tạo phòng thành công.");
        }

        public async Task UpdateRoomStatus(int hotelId, int roomId, string status)
        {
            string url = $"{baseUrl}{hotelId}/rooms/{roomId}/status";
            var updateStatusVM = new UpdateDatPhongStatusVM
            {
                Status = status
            };
            string jsonRequest = JsonConvert.SerializeObject(updateStatusVM);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                string errorDetail = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Chi tiết lỗi: {errorDetail}");
                return;
            }

            Console.WriteLine("Cập nhật trạng thái phòng thành công.");
        }

        public async Task DeleteRoom(int hotelId, int roomId)
        {
            string url = $"{baseUrl}{hotelId}/rooms/{roomId}";
            HttpResponseMessage response = await HttpClientSingleton.Instance.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                return;
            }

            Console.WriteLine("Xóa phòng thành công.");
        }
    }
}
