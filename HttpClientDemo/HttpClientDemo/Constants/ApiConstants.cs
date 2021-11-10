using Xamarin.Forms;

namespace HttpClientDemo.Constants
{
    public static class ApiConstants
    {
        public static string BaseApiUrl = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001/" : "https://localhost:5001/";
        public const string ItemsEndpoint = "api/items";
    }
}
