using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHttpClientDemo
{
    public class SimpleRepository
    {
        private HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/")
        };

        public async Task<List<Item>> GetItems()
        {
            string itemJson = await _httpClient.GetStringAsync($"api/item");
            return JsonConvert.DeserializeObject<List<Item>>(itemJson);
        }

        public async Task<Item> GetItemById(string id)
        {
            var itemJson = await _httpClient.GetStringAsync($"api/item/{id}");
            return JsonConvert.DeserializeObject<Item>(itemJson);
        }

        public async Task AddItem(Item item)
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/item", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong!");
            }
        }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
