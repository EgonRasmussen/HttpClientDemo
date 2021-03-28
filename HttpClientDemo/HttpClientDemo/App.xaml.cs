using HttpClientDemo.Services;
using Repository;
using TinyIoC;
using Xamarin.Forms;
using Akavache;

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

            BlobCache.ApplicationName = "MyDemoAkavache";   // Added

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
