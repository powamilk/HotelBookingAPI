using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HotelBookingConsole.Model.KhachHang;
using Newtonsoft.Json;

namespace HotelBookingConsole.UI
{
    internal class KhachHangUIService
    {
        private readonly string baseUrl = "https://localhost:44351/api/";


        public async Task GetAll()
        {
            string urlGet = $"{baseUrl}customers";
            HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(urlGet);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
                return;
            }

            string data = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<KhachHangVM>>(data);

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id} - {customer.Name} - {customer.Email} - {customer.Phone}");
            }
        }
        public async Task GetById(int id)
        {
            string urlGet = $"{baseUrl}customers/{id}";
            HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(urlGet);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
                return;
            }

            string data = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<KhachHangVM>(data);

            Console.WriteLine($"{customer.Name} - {customer.Email} - {customer.Phone}");
        }

        public async Task Post(KhachHangCreateVM vm)
        {
            string urlPost = $"{baseUrl}customers";
            string jsonRequest = JsonConvert.SerializeObject(vm);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PostAsync(urlPost, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Lỗi: " + response.StatusCode);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Chi tiết lỗi" + responseContent);
            }
            else
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
            }
        }

        public async Task Put(int id, KhachHangUpdateVM vm)
        {
            string urlPut = $"{baseUrl}customers/{id}";
            string jsonRequest = JsonConvert.SerializeObject(vm);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PutAsync(urlPut, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Lỗi: " + response.StatusCode);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Chi tiết lỗi" + responseContent);
            }
            else
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
            }
        }

        public async Task Delete(int id)
        {
            string urlDelete = $"{baseUrl}customers/{id}";
            HttpResponseMessage response = await HttpClientSingleton.Instance.DeleteAsync(urlDelete);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Lỗi: " + response.StatusCode);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Chi tiết lỗi" + responseContent);
            }
            else
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
            }
        }
    }
}
