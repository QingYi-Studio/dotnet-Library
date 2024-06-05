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

        public (string, string, string, DateTime, DateTime, DateTime) SelectFile(string SelectedFilePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(SelectedFilePath);

                SelectedFileName = fileInfo.Name;
                SelectedFileExtension = fileInfo.Extension;
                SelectedFileSize = fileInfo.Length;
                SelectedFileCreationTime = fileInfo.CreationTime;
                SelectedFileLastAccessTime = fileInfo.LastAccessTime;
                SelectedFileLastWriteTime = fileInfo.LastWriteTime;

                return (SelectedFileName, SelectedFileExtension, SelectedFileSize.ToString(), SelectedFileCreationTime, SelectedFileLastAccessTime, SelectedFileLastWriteTime);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
