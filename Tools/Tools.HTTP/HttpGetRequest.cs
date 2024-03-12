namespace Tools.HTTP
{
    public class HttpGetRequest
    {
        public async Task<string> GetResponseAsync(string url)
        {
            string response = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 发起 GET 请求并获取响应
                    HttpResponseMessage httpResponse = await client.GetAsync(url);
                    response = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                // 处理 HttpRequestException 异常
                Console.WriteLine("HttpRequestException caught: " + ex.Message);
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return response;
        }
    }

    class T
    {
        static async Task Main(string[] args)
        {
            // 实例化 HttpGetRequest 类
            HttpGetRequest httpRequest = new HttpGetRequest();

            // 要请求的 URL
            string url = "https://api.example.com/data";

            // 发起 GET 请求并获取响应
            string response = await httpRequest.GetResponseAsync(url);

            // 输出响应内容
            Console.WriteLine("Response from " + url + ":");
            Console.WriteLine(response);
        }
    }
}
