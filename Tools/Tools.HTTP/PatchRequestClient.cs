using System.Net.Http.Headers;
using System.Text;

namespace Tools.HTTP
{
    public class PatchRequestClient
    {
        private HttpClient _client;

        public PatchRequestClient()
        {
            _client = new HttpClient();
        }

        public async Task<string> SendPatchRequestAsync(string url, string requestBody, HttpHeaders? customHeaders = null)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            if (customHeaders != null)
            {
                foreach (var header in customHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"PATCH request failed with status code {response.StatusCode}");
            }
        }
    }
}
