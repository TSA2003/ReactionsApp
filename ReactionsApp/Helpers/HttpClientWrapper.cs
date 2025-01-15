using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Helpers
{
    public static class HttpClientWrapper
    {
        private const string BASE_URL_WINDOWS = "https://localhost:7108";
        private const string BASE_URL_ANDROID = "https://10.0.2.2:7108";
        private const int TIMEOUT_SECONDS = 10;

        private static HttpClient client;

        static HttpClientWrapper()
        {
            client = Create();
        }

        private static HttpClient Create() 
        {
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            string baseUrl = BASE_URL_WINDOWS;

            if (DeviceInfo.Platform == DevicePlatform.Android) 
            { 
                baseUrl = BASE_URL_ANDROID;
            }

            return new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(TIMEOUT_SECONDS)
            }; 
        }

        public static async Task<HttpResponseMessage> GetAuthorizedAsync(string endpoint, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetAsync(endpoint);
            client.DefaultRequestHeaders.Authorization = null;
            return result;
        }

        public static async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        { 
            return await client.PostAsync(endpoint, content);
        }

        public static async Task<HttpResponseMessage> PostAuthorizedAsync(string endpoint, HttpContent content, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostAsync(endpoint, content);
            client.DefaultRequestHeaders.Authorization = null;
            return result;
        }
    }
}
