using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpClientDemo.Services
{
    public interface IItemsService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync();
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        
    }
}
