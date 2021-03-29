﻿using Akavache;
using HttpClientDemo.Services;
using Repository;
using TinyIoC;
using Xamarin.Forms;

namespace HttpClientDemo
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var container = TinyIoCContainer.Current;
            container.Register<IGenericRepository, GenericRepository>();
            container.Register<IItemsService, ItemsService>();

            Akavache.Registrations.Start("AkavacheExperiment");

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            BlobCache.Shutdown().Wait();
        }

        protected override void OnResume()
        {
        }
    }
}
