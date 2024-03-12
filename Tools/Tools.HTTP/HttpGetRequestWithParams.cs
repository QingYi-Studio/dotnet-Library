namespace Tools.HTTP
{
    public class HttpGetRequestWithParams
    {
        private readonly HttpClient httpClient;

        public HttpGetRequestWithParams()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string url, Dictionary<string, string> queryParams)
        {
            if (queryParams != null && queryParams.Count > 0)
            {
                var query = new FormUrlEncodedContent(queryParams);
                url += "?" + await query.ReadAsStringAsync();
            }

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
