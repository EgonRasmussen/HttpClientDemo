using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleHttpClientDemo
{
    public interface IBackendService
    {
        [Get("/api/items")]
        Task<List<Item>> GetItems();

        [Get("/api/items/{id}")]
        Task<Item> GetItemById(string id);

        [Post("/api/items")]
        Task AddItem([Body] Item item);
    }

    //public class SimpleRepository
    //{
    //    private HttpClient _httpClient = new HttpClient
    //    {
    //        BaseAddress = new Uri("https://localhost:5001/")
    //    };

    //    public async Task<List<Item>> GetItems()
    //    {
    //        string itemJson = await _httpClient.GetStringAsync($"api/items");
    //        return JsonConvert.DeserializeObject<List<Item>>(itemJson);
    //    }

    //    public async Task<Item> GetItemById(string id)
    //    {
    //        var itemJson = await _httpClient.GetStringAsync($"api/items/{id}");
    //        return JsonConvert.DeserializeObject<Item>(itemJson);
    //    }

    //    public async Task AddItem(Item item)
    //    {
    //        var itemJson = JsonConvert.SerializeObject(item);
    //        var content = new StringContent(itemJson, Encoding.UTF8, "application/json");
    //        HttpResponseMessage response = await _httpClient.PostAsync("api/items", content);
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            throw new Exception("Something went wrong!");
    //        }
    //    }
    //}

    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
