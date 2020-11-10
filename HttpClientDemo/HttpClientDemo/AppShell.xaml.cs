using System;
using System.Collections.Generic;
using HttpClientDemo.ViewModels;
using HttpClientDemo.Views;
using Xamarin.Forms;

namespace HttpClientDemo
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
