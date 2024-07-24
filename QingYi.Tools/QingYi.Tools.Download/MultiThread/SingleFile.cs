namespace QingYi.Tools.Download.MultiThread
{
    internal class SingleFile
    {
        private string? _url;
        private string? _destinationPath;
        private long _totalBytes;
        private long _downloadedBytes;
        private int _numThreads;
        private readonly HttpClient _httpClient;
        private DateTime _startTime;

        public SingleFile()
        {
            _httpClient = new HttpClient();
            _numThreads = 8; // Default number of threads
        }

        public void SetUrl(string url)
        {
            _url = url;
        }

        public void SetDestinationPath(string destinationPath)
        {
            _destinationPath = destinationPath;
        }

        public void SetNumThreads(int numThreads)
        {
            _numThreads = Math.Max(numThreads, 1); // Ensure numThreads is always at least 1
        }

        public async Task StartDownloadAsync()
        {
            _startTime = DateTime.Now; // Record start time

            using (var response = await _httpClient.GetAsync(_url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                _totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault();
            }

            var tasks = new Task[_numThreads];
            for (int i = 0; i < _numThreads; i++)
            {
                tasks[i] = DownloadChunkAsync(i);
            }

            await Task.WhenAll(tasks);
        }

        private async Task DownloadChunkAsync(int chunkIndex)
        {
            using var response = await _httpClient.GetAsync(_url, HttpCompletionOption.ResponseHeadersRead);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            long chunkSize = _totalBytes / _numThreads;
            long startPosition = chunkIndex * chunkSize;
            long endPosition = (chunkIndex == _numThreads - 1) ? _totalBytes - 1 : startPosition + chunkSize - 1;

            responseStream.Seek(startPosition, SeekOrigin.Begin);

            using var fileStream = new FileStream(_destinationPath!, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            var buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = await responseStream.ReadAsync(buffer)) > 0 && responseStream.Position <= endPosition)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                _downloadedBytes += bytesRead;
            }
        }

        public double GetDownloadProgress()
        {
            return (double)_downloadedBytes / _totalBytes * 100;
        }

        public double GetDownloadSpeed(DownloadSpeedUnit speedUnit = DownloadSpeedUnit.KBps)
        {
            // Calculate download speed
            // Speed = Total Bytes downloaded / Elapsed time
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            decimal speed = (decimal)_downloadedBytes / (decimal)elapsedTime.TotalSeconds;

            // Convert speed to the specified unit and round to 2 decimal places
            decimal roundedSpeed = Math.Round(speedUnit switch
            {
                DownloadSpeedUnit.Bps => speed,
                DownloadSpeedUnit.KBps => speed / 1024,
                DownloadSpeedUnit.MBps => speed / (1024 * 1024),
                DownloadSpeedUnit.GBps => speed / (1024 * 1024 * 1024),
                DownloadSpeedUnit.TBps => speed / (1024m * 1024 * 1024 * 1024),
                _ => throw new ArgumentOutOfRangeException(nameof(speedUnit), speedUnit, null),
            }, 2);

            return (double)roundedSpeed;
        }
    }

    public enum DownloadSpeedUnit
    {
        Bps,
        KBps,
        MBps,
        GBps,
        TBps
    }
}
