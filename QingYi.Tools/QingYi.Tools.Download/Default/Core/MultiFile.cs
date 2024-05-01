namespace QingYi.Tools.Download.Default.Core
{
    internal class MultiFile
    {
        public static void Download(string[] fileUrls, string exportFolder)
        {
            using HttpClient client = new();
            foreach (string url in fileUrls)
            {
                string fileName = Path.GetFileName(url);
                string filePath = Path.Combine(exportFolder, fileName);

                // 同步方式下载文件
                HttpResponseMessage response = client.GetAsync(url).Result;
                using Stream contentStream = response.Content.ReadAsStreamAsync().Result;
                using Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                contentStream.CopyTo(fileStream);
            }
        }

        public static async Task DownloadAsync(string[] fileUrls, string exportFolder, int maxConcurrentDownloads = 8)
        {
            // 修正 maxConcurrentDownloads 为 8 到 1024 之间
            maxConcurrentDownloads = Math.Max(1, Math.Min(1024, maxConcurrentDownloads));

            using HttpClient client = new();
            List<Task> downloadTasks = [];

            // 使用信号量控制同时下载的数量
            using SemaphoreSlim semaphore = new SemaphoreSlim(maxConcurrentDownloads);
            foreach (string url in fileUrls)
            {
                await semaphore.WaitAsync();

                downloadTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        string fileName = Path.GetFileName(url);
                        string filePath = Path.Combine(exportFolder, fileName);
                        HttpResponseMessage response = await client.GetAsync(url);
                        using Stream contentStream = await response.Content.ReadAsStreamAsync();
                        using Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                        await contentStream.CopyToAsync(fileStream);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            // 等待所有下载任务完成
            await Task.WhenAll(downloadTasks);
        }
    }
}
