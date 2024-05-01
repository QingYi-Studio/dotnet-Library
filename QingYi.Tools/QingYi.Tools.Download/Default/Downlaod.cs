using QingYi.Tools.Download.Default.Core;

namespace QingYi.Tools.Download.Default
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
    }
}
