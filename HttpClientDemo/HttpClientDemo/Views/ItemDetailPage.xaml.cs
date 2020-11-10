using System.ComponentModel;
using Xamarin.Forms;
using HttpClientDemo.ViewModels;

namespace HttpClientDemo.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}