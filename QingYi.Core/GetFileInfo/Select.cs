using System;
using System.IO;

namespace QingYi.Core.GetFileInfo
{
    internal class Select
    {
        public string SelectedFilePath { get; private set; }
        public string SelectedFileName { get; private set; }
        public string SelectedFileExtension { get; private set; }
        public long SelectedFileSize { get; private set; }
        public DateTime SelectedFileCreationTime { get; private set; }
        public DateTime SelectedFileLastAccessTime { get; private set; }
        public DateTime SelectedFileLastWriteTime { get; private set; }

        public Tuple<string, string, long, DateTime, DateTime, DateTime> SelectFile(string selectedFilePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(selectedFilePath);

                SelectedFilePath = selectedFilePath;
                return Tuple.Create(
                    fileInfo.Name,
                    fileInfo.Extension,
                    fileInfo.Length,
                    fileInfo.CreationTime,
                    fileInfo.LastAccessTime,
                    fileInfo.LastWriteTime
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
