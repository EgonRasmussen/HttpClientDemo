using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HttpClientDemo.Services;
using HttpClientDemo.Views;
using TinyIoC;
using HttpClientDemo.Models;
using Repository;

namespace HttpClientDemo
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var container = TinyIoCContainer.Current;
            container.Register<IGenericRepository, Repository.GenericRepository>();
            container.Register<IItemsService<Item>, ItemsService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
