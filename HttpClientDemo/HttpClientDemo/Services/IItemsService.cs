using HttpClientDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpClientDemo.Services
{
    public interface IItemsService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<bool> AddItemAsync(Item item);
        Task<bool> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(string id);
        Task<Item> GetItemAsync(string id);
        
    }
}
