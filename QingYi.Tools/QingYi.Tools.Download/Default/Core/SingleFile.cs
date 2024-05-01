namespace QingYi.Tools.Download.Default.Core
{
    internal class SingleFile
    {
        public static void Download(string url, string destinationPath)
        {
            using var client = new HttpClient();
            try
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = response.Content.ReadAsStreamAsync().Result;
                    using var fileStream = File.Create(destinationPath);
                    responseStream.CopyTo(fileStream);
                }
                else
                {
                    throw new Exception($"Failed to download file. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading file: {ex.Message}");
            }
        }

        public static async Task DownloadAsync(string url, string destinationPath)
        {
            using var client = new HttpClient();
            try
            {
                using var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using var fileStream = File.Create(destinationPath);
                    await response.Content.CopyToAsync(fileStream);
                }
                else
                {
                    throw new Exception($"Failed to download file. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading file: {ex.Message}");
            }
        }
    }
}
