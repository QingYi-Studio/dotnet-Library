using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QingYi.Core.Web
{
    public class Test
    {
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
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("http://" + url);
                    response.EnsureSuccessStatusCode(); // 确保成功响应

                    // 请求结束时间
                    DateTime endTime = DateTime.Now;

                    // 计算连接延迟
                    TimeSpan delay = endTime - startTime;
                    return delay.TotalMilliseconds;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Fast Test<br></br>
        /// 快速测试是否可连接
        /// </summary>
        /// <param name="url">Formatted links|格式化过的链接</param>
        /// <returns>Success is 1 and failure is 0|成功为1失败为0</returns>
        public static int FastTest(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // 如果响应成功，则返回1
                        return 1;
                    }
                    else
                    {
                        // 如果响应失败，则返回0
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                // 请求异常，返回0
                return 0;
            }
        }

        /// <summary>
        /// Fast Test Async<br></br>
        /// 快速测试是否可连接（异步）
        /// </summary>
        /// <param name="url">Formatted links|格式化过的链接</param>
        /// <returns>Success is 1 and failure is 0|成功为1失败为0</returns>
        public static async Task<int> FastTestAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // 请求成功，返回1
                        return 1;
                    }
                    else
                    {
                        // 请求失败，返回0
                        return 0;
                    }
                }
            }
            catch (HttpRequestException)
            {
                // 请求异常，返回0
                return 0;
            }
        }
    }
}
