using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tools.HTTP
{
    public class CookieRequestClient
    {
        private HttpClient _client;
        private CookieContainer _cookieContainer;

        public CookieRequestClient()
        {
            _cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                UseCookies = true
            };
            _client = new HttpClient(handler);
        }

        public async Task<string> SendRequestWithCookieAsync(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
