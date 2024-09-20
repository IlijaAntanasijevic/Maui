using Maui.Common;
using RestSharp;


namespace Maui
{
    public static class Api
    {
        private static RestClient _client;
        private static bool isAuthorized = false;

        private static string BaseUrl => "http://localhost:5000/";
        public static string BaseImageUrl { get; } = "http://localhost:5000/uploads/";

        public static RestClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = new RestClient(BaseUrl);
                }

                var user = SecureStorage.Default.GetUser();

                if(user != null && !isAuthorized)
                {
                    isAuthorized = true;
                    _client.AddDefaultHeader("Authorization", "Bearer " + user.Token);
                }

                return _client;
            }
        }
    }
}
