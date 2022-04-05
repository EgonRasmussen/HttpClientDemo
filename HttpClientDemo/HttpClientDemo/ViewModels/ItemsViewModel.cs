using HttpClientDemo.Models;
using HttpClientDemo.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HttpClientDemo.ViewModels;

public class ItemsViewModel : BaseViewModel
{
    public ItemsViewModel()
    {
        Title = "Browse";
        Items = new ObservableCollection<Item>();

        IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;     //

        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;   //
    }

    private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)    //
    {
        IsConnected = e.NetworkAccess != NetworkAccess.Internet;
    }

    public ObservableCollection<Item> Items { get; }

    private Item _selectedItem;
    public Item SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }


    private Command loadItemsCommand;
    public ICommand LoadItemsCommand => loadItemsCommand ??= new Command(ExecuteLoadItemsCommand);
    async void ExecuteLoadItemsCommand()
    {
        IsBusy = true;

        try
        {
            Items.Clear();
            var items = await _itemsService.GetItemsAsync();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"********** Error: {ex}*******");
        }
        finally
        {
            Debug.WriteLine("********* Load Items ********");
            IsBusy = false;
        }
    }

    private Command _itemTappedCommand;
    public ICommand ItemTappedCommand => _itemTappedCommand ??= new Command<Item>
        (
            execute: async (item) =>
            {
                if (item == null)
                    return;

                // This will push the ItemDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
            }
        );

    private Command _addItemCommand;
    public ICommand AddItemCommand => _addItemCommand ??= new Command
    (
        execute: async () => await Shell.Current.GoToAsync(nameof(NewItemPage))
    );

    public void OnAppearing()
    {
        IsBusy = true;
        SelectedItem = null;
    }

    public void Dispose()       //
    {
        Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
    }
}
