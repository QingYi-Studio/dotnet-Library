using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tools.HTTP
{
    public class JsonPostRequest
    {
        public async Task<string> PostJsonData(string url, string jsonData)
        {
            if (jsonData == null)
            {
                throw new ArgumentNullException(nameof(jsonData));
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> PostJsonDataFromFile(string url, string filePath)
        {
            try
            {
                var jsonData = File.ReadAllText(filePath);
                return await PostJsonData(url, jsonData); // 直接传递 jsonData 字符串
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
    class Test
    {
        public static async Task JsonData()
        {
            try
            {
                JsonPostRequest jsonRequest = new JsonPostRequest();

                string url = "https://example.com/api/endpoint";
                string jsonData = "{\"key\": \"value\"}";

                string response = await jsonRequest.PostJsonData(url, jsonData);

                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static async Task LocalJson()
        {
            try
            {
                JsonPostRequest jsonRequest = new JsonPostRequest();

                string url = "https://example.com/api/endpoint";
                string filePath = "path/to/your/json/file.json";

                string response = await jsonRequest.PostJsonDataFromFile(url, filePath);

                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
