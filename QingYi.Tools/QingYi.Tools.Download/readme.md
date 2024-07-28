# QingYi.Tools.Download

A library that combines multiple download types.

一个综合了多种下载类型的库。

## Single Thread

### Single File

```
string url = "https://example.com/file.zip";
string filePath = "downloaded_file.zip";

// Sync download|同步下载
Console.WriteLine("Starting synchronous download...");
var syncDownloader = new Downloader(url, filePath);
syncDownloader.DownloadSync((speed, bytes) =>
{
Console.WriteLine($"Downloaded {bytes} bytes at {speed:F2} KB/s");
});
Console.WriteLine("Synchronous download complete.");

// Async download|异步下载
Console.WriteLine("Starting asynchronous download...");
var cancellationTokenSource = new CancellationTokenSource();
var asyncDownloader = new Downloader(url, filePath);
var downloadTask = asyncDownloader.DownloadAsync(
    new Progress<double>(speed => Console.WriteLine($"Speed: {speed:F2} KB/s")),
    new Progress<long>(bytes => Console.WriteLine($"Downloaded: {bytes} bytes")),
    cancellationTokenSource.Token);

// Simulate the operation during the download process|模拟下载过程中的操作
await Task.Delay(5000); // Wait 5 seconds|等待5秒
cancellationTokenSource.Cancel(); // Cancel downloading|取消下载

try
{
    await downloadTask; // Wait for the download task to complete|等待下载任务完成
    Console.WriteLine("Asynchronous download complete.");
}
catch (OperationCanceledException)
{
    Console.WriteLine("Asynchronous download canceled.");
}
```

## Multi Thread

### Single File

```
// Create a MultiThreadedDownloader instance|创建 MultiThreadedDownloader 实例
var downloader = new SingleThread.SIngleFile();

// Set download link|设置下载链接
downloader.SetUrl("https://example.com/bigfile.zip");

// Set save path|设置保存路径
downloader.SetDestinationPath("C:/Downloads/bigfile.zip");

// Set thread number|设置线程数（可选）
downloader.SetNumThreads(4); // 如果不设置，默认为8

// Start download|开始下载
await downloader.StartDownloadAsync();

// Get download progress|获取下载进度
double progress = downloader.GetDownloadProgress();
Console.WriteLine($"Download Progress: {progress}%");

// Get download speed|获取下载速度
double speed = downloader1.GetDownloadSpeed(DownloadSpeedUnit.MBps);
Console.WriteLine($"Download Speed: {speed} MB/s");
```
