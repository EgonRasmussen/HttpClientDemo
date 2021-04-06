using HttpClientDemo.Constants;
using HttpClientDemo.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TinyIoC;
using MonkeyCache.SQLite;
using Xamarin.Essentials;

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

            string url = builder.Path;

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return Barrel.Current.Get<IEnumerable<Item>>(key: url);
            }
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<IEnumerable<Item>>(key: url);
            }
            Thread.Sleep(3000); // Simulerer 3 sekunders forsinkelte
            var items = await _genericRepository.GetAsync<IEnumerable<Item>>(builder.ToString());
            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: url, data: items, expireIn: TimeSpan.FromSeconds(20));
            return items;
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