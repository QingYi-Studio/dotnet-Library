using System;
using System.Net;
using System.Net.Http;

namespace Tools.HTTP
{
    public class ProxyHttpClient
    {
        private HttpClient httpClient;

        public ProxyHttpClient()
        {
            var handler = new HttpClientHandler
            {
                UseProxy = true,
                Proxy = new WebProxy("http://yourproxyaddress:port")
            };

            httpClient = new HttpClient(handler);
        }

        public string Get(string url)
        {
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception($"Failed to fetch data. Status code: {response.StatusCode}");
            }
        }

        public string Post(string url, string data)
        {
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception($"Failed to post data. Status code: {response.StatusCode}");
            }
        }
    }
}
