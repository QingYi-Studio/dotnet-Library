using QingYi.Tools.Download.Default;
using QingYi.Tools.Download.Multithread;

namespace QingYi.Tools.Download
{
    public class Downlaod
    {
        public class DefaultDownload
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
                    SingleFileDefault.Download(url, "download");
                }
                else
                {
                    SingleFileDefault.Download(url, destinationPath);
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
                    await SingleFileDefault.DownloadAsync(url, "download");
                }
                else
                {
                    await SingleFileDefault.DownloadAsync(url, destinationPath);
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
                    MultiFileDefault.Download(fileUrls, "download");
                }
                else
                {
                    MultiFileDefault.Download(fileUrls, exportFolder);
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
                    await MultiFileDefault.DownloadAsync(fileUrls, "download", maxConcurrentDownloads);
                }
                else
                {
                    await MultiFileDefault.DownloadAsync(fileUrls, exportFolder, maxConcurrentDownloads);
                }
            }
        }

        public class MultithreadDownload
        {
            /// <summary>
            /// Single file multi-threaded blocking download<br></br>
            /// 单文件多线程阻塞式下载
            /// </summary>
            /// <param name="url">File link|文件链接</param>
            /// <param name="outputPath">Output path|输出路径</param>
            /// <param name="thread">Thread number|线程数</param>
            public static void SingleFileDownload(string url, string? outputPath, int thread = 16)
            {
                outputPath ??= "download";
                CreateRootFolder.CreateFolder();
                SingleFileMultithread singleFile = new(url, outputPath);
                singleFile.SetNumThreads(thread);
                singleFile.Download();
            }

            public static async Task SingleFileDownloadAsync(string fileUrl, string? outputPath, int? threadCount)
            {
                outputPath ??= "download";
                CreateRootFolder.CreateFolder();

                // 创建 SingleFileMultiThreadAsync 实例
                var downloader = new SingleFileMultiThreadAsync(threadCount ?? 16); // 如果线程数为空则默认16

                // 下载文件
                await downloader.DownloadFileAsync(fileUrl, outputPath);
            }
        }
    }
}
