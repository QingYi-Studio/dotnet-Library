using System.Net;

namespace QingYi.Core.Web
{
    public class Test
    {
        /// <summary>
        /// Sync Test<br></br>
        /// 同步测试
        /// </summary>
        /// <param name="url">Url|网址</param>
        /// <returns>Test result|测试结果</returns>
        /// <exception cref="Exception"></exception>
        public static double TestConnection(string url)
        {
            // 发送请求并记录延迟
            DateTime startTime = DateTime.Now;

            try
            {
                using HttpClient client = new();
                HttpResponseMessage response = client.GetAsync("http://" + url).Result;
                response.EnsureSuccessStatusCode(); // 确保成功响应

                // 请求结束时间
                DateTime endTime = DateTime.Now;

                // 计算连接延迟
                TimeSpan delay = endTime - startTime;
                return delay.TotalMilliseconds;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Async Test<br></br>
        /// 异步测试
        /// </summary>
        /// <param name="url">Url|网址</param>
        /// <returns>Test result|测试结果</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<double> TestConnectionAsync(string url)
        {
            // 发送请求并记录延迟
            DateTime startTime = DateTime.Now;

            try
            {
                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync("http://" + url);
                response.EnsureSuccessStatusCode(); // 确保成功响应

                // 请求结束时间
                DateTime endTime = DateTime.Now;

                // 计算连接延迟
                TimeSpan delay = endTime - startTime;
                return delay.TotalMilliseconds;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
