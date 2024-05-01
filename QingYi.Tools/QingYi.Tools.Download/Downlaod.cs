using QingYi.Tools.Download.Default.Core;

namespace QingYi.Tools.Download
{
    public class Downlaod
    {
        /// <summary>
        /// Download File Sync<br></br>
        /// 同步下载文件
        /// </summary>
        /// <param name="url">File link|文件链接</param>
        /// <param name="destinationPath">Folder|文件夹</param>
        public static void SingleFileDownload(string url, string? destinationPath = null)
        {
            if (destinationPath == null)
            {
                CreateRootFolder.CreateFolder();
                SingleFile.Download(url, "download");
            }
            else
            {
                SingleFile.Download(url, destinationPath);
            }
        }

        /// <summary>
        /// Download File Async<br></br>
        /// 异步下载文件
        /// </summary>
        /// <param name="url">File link|文件链接</param>
        /// <param name="destinationPath">Folder|文件夹</param>
        public static async Task SingleFileDownloadAsync(string url, string? destinationPath = null)
        {
            if (destinationPath == null)
            {
                CreateRootFolder.CreateFolder();
                await SingleFile.DownloadAsync(url, "download");
            }
            else
            {
                await SingleFile.DownloadAsync(url, destinationPath);
            }
        }

        /// <summary>
        /// download multifiles<br></br>
        /// 多文件下载
        /// </summary>
        /// <param name="fileUrls">All file url|所有文件链接</param>
        /// <param name="exportFolder">Folder|文件夹</param>
        public static void MultiFileDownload(string[] fileUrls, string? exportFolder = null)
        {
            if (exportFolder == null)
            {
                CreateRootFolder.CreateFolder();
                MultiFile.Download(fileUrls, "download");
            }
            else
            {
                MultiFile.Download(fileUrls, exportFolder);
            }
        }

        /// <summary>
        /// download multifiles(Async)<br></br>
        /// 多文件下载(异步)
        /// </summary>
        /// <param name="fileUrls">All file url|所有文件链接</param>
        /// <param name="exportFolder">Folder|文件夹</param>
        /// <param name="maxConcurrentDownloads">Number of files downloaded at the same time|同时下载的文件数量</param>
        public static async Task MultiFileDownloadAsync(string[] fileUrls, string? exportFolder = null, int maxConcurrentDownloads = 8)
        {
            if (exportFolder == null)
            {
                CreateRootFolder.CreateFolder();
                await MultiFile.DownloadAsync(fileUrls, "download", maxConcurrentDownloads);
            }
            else
            {
                await MultiFile.DownloadAsync(fileUrls, exportFolder, maxConcurrentDownloads);
            }
        }
    }
}
