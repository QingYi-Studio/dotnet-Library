using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tools.HTTP
{
    public class PutRequestClient
    {
        private HttpClient _client;

        public PutRequestClient()
        {
            _client = new HttpClient();
        }

        public async Task<string> SendPutRequestAsync(string url, string requestBody)
        {
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"PUT request failed with status code {response.StatusCode}");
            }
        }
    }
}
