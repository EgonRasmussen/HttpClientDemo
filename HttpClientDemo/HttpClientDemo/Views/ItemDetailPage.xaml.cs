using HttpClientDemo.ViewModels;
using Xamarin.Forms;

namespace HttpClientDemo.Views;

public partial class ItemDetailPage : ContentPage
{
    public ItemDetailPage()
    {
        InitializeComponent();
        BindingContext = new ItemDetailViewModel();
    }
}
