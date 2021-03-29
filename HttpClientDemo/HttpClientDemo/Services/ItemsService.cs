using HttpClientDemo.Constants;
using HttpClientDemo.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TinyIoC;

namespace HttpClientDemo.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IGenericRepository _genericRepository;
        public ItemsService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.ItemsEndpoint
            };
            //Thread.Sleep(3000); // Simulerer 3 sekunders forsinkelse
            return await _genericRepository.GetAsync<IEnumerable<Item>>(builder.ToString());
        }

        public async Task<Item> GetItemAsync(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}/{id}"
            };
            return await _genericRepository.GetAsync<Item>(builder.ToString());
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.ItemsEndpoint
            };
            await _genericRepository.PostAsync(builder.ToString(), item);
            return true;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}/{item.Id}"
            };
            await _genericRepository.PutAsync(builder.ToString(), item);
            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.ItemsEndpoint}/{id}"
            };
            await _genericRepository.DeleteAsync(builder.ToString());
            return true;
        }
    }
}