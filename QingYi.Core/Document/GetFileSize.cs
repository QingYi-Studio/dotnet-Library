namespace QingYi.Core.Document
{
    public class GetFileSize
    {
        /// <summary>
        /// Get file size<br></br>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath">File path|文件路径</param>
        /// <param name="format">File size formate(default KB)|文件大小格式(默认KB)</param>
        /// <returns>File size|文件大小</returns>
        /// <exception cref="ArgumentException">Not support web files or invalid formate</exception>
        public static double GetSize(string filePath, FileSizeFormat format = FileSizeFormat.Kilobytes)
        {
            if (IsWebFile(filePath))
            {
                throw new ArgumentException("Operation not supported for web files.");
            }

            double fileSize = new FileInfo(filePath).Length;

            return format switch
            {
                FileSizeFormat.Bytes => fileSize,
                FileSizeFormat.Kilobytes => fileSize / 1024,
                FileSizeFormat.Megabytes => fileSize / 1024 / 1024,
                FileSizeFormat.Gigabytes => fileSize / 1024 / 1024 / 1024,
                _ => throw new ArgumentException("Invalid file size format."),
            };
        }

        private static bool IsWebFile(string filePath)
        {
            string root = Path.GetPathRoot(filePath)!;
            return root.StartsWith("http://") || root.StartsWith("https://");
        }
    }

    public enum FileSizeFormat
    {
        Bytes,
        Kilobytes,
        Megabytes,
        Gigabytes
    }
}
