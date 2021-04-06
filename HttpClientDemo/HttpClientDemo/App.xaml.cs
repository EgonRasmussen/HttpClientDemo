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

            MonkeyCache.SQLite.Barrel.ApplicationId = "MyApp";

            var container = TinyIoCContainer.Current;
            container.Register<IGenericRepository, GenericRepository>();
            container.Register<IItemsService, ItemsService>();

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
