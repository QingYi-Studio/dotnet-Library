namespace QingYi.Tools.Download.SingleThread
{
    internal class SingleFile(string url, string filePath)
    {
        private readonly HttpClient _httpClient = new();
        private long _totalBytesRead;

        public void DownloadSync(Action<double, long> progressCallback)
        {
            using var response = _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
            response.EnsureSuccessStatusCode();
            using var contentStream = response.Content.ReadAsStreamAsync().Result;
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            var buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = contentStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
                _totalBytesRead += bytesRead;
                progressCallback?.Invoke(GetCurrentSpeedKBps(), _totalBytesRead);
            }
        }

        public async Task DownloadAsync(IProgress<double> speedProgress, IProgress<long> sizeProgress, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            var buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken)) > 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                _totalBytesRead += bytesRead;
                sizeProgress?.Report(_totalBytesRead);
                speedProgress?.Report(GetCurrentSpeedKBps());
            }
        }

        private double GetCurrentSpeedKBps()
        {
            // Calculate download speed
            var totalMilliseconds = Environment.TickCount;
            var bytesPerSecond = (_totalBytesRead * 1000) / totalMilliseconds;
            return bytesPerSecond / 1024; // Convert bytes/s to KB/s
        }
    }
}
