using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using HttpClientDemo.Models;
using HttpClientDemo.Views;
using System.Collections.Generic;
using System.Reactive.Linq;   // IMPORTANT - this makes await work!
using Xamarin.Essentials;
using Akavache;

namespace HttpClientDemo.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                GetItems().Subscribe((items) =>
                {
                    Items.Clear();

                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                });
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public IObservable<IEnumerable<Item>> GetItems()
        {
            return BlobCache.LocalMachine.GetAndFetchLatest("items",
            async () => await _itemsService.GetItemsAsync(), 
            (offset) =>
            {
                // return true; // To indicate the cache is expired and new data should follow.  
                // return false; // When no network is available or cache is not expired, then get data from Cache

                if (Connectivity.NetworkAccess == NetworkAccess.None)
                {
                    return false;
                }
                return (DateTimeOffset.Now - offset).Seconds > 10;
            });
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}