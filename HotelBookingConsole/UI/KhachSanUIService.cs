using HotelBookingConsole.Model.KhachSan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelBookingConsole.UI
{
    internal class KhachSanUIService
    {
        private readonly string baseUrl = "https://localhost:44351/api/";

        public async Task GetAll()
        {
            string urlGet = $"{baseUrl}hotels";
            HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(urlGet);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
                return;
            }

            string data = await response.Content.ReadAsStringAsync();
            var vms = JsonConvert.DeserializeObject<List<KhachSanVM>>(data);

            foreach (var vm in vms)
            {
                Console.WriteLine($"{vm.Id} - {vm.Name} - {vm.Location} - {vm.TotalRoom} - {vm.Description} - {vm.Rating}");
            }
        }

        public async Task GetById(int id)
        {
            string urlGet = $"{baseUrl}hotels/{id}";
            HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(urlGet);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
                return;
            }

            string data = await response.Content.ReadAsStringAsync();
            var vm = JsonConvert.DeserializeObject<KhachSanVM>(data);

            Console.WriteLine($"{vm.Id} - {vm.Name} - {vm.Location} - {vm.TotalRoom} - {vm.Description} - {vm.Rating}");
        }

        public async Task Post(KhachSanCreateVM vm)
        {
            string urlPost = $"{baseUrl}hotels";
            string jsonRequest = JsonConvert.SerializeObject(vm);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PostAsync(urlPost, content);

            string data = await response.Content.ReadAsStringAsync();
            Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
            Console.WriteLine(data);
        }

        public async Task Put(int id, KhachSanUpdateVM vm)
        {
            string urlPut = $"{baseUrl}hotels/{id}";
            string jsonRequest = JsonConvert.SerializeObject(vm);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await HttpClientSingleton.Instance.PutAsync(urlPut, content);

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Lỗi: " +response.StatusCode);
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
            string urlDelete = $"{baseUrl}hotels/{id}";
            HttpResponseMessage response = await HttpClientSingleton.Instance.DeleteAsync(urlDelete);

            Console.WriteLine("[" + (int)response.StatusCode + "] " + response.StatusCode);
        }
    }
}
