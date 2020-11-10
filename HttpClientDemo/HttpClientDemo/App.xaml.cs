using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HttpClientDemo.Services;
using HttpClientDemo.Views;

namespace HttpClientDemo
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<DataStore>();
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
