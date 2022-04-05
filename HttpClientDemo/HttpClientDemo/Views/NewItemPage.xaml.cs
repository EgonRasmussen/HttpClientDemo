using HttpClientDemo.Models;
using HttpClientDemo.ViewModels;
using Xamarin.Forms;

namespace HttpClientDemo.Views;

public partial class NewItemPage : ContentPage
{
    public Item Item { get; set; }

    public NewItemPage()
    {
        InitializeComponent();
        BindingContext = new NewItemViewModel();
    }
}
