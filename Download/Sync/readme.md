# Download.Sync

A library can download multi files or single files.

Just a simple library, don't bully me, I know it's really bad but I really need this.

## Usage

```c#
using System;

class Program
{
    static void Main()
    {
        // Download multiple files to different directories at the same time
        string[] fileUrlsDifferentDirs = { "http://www.example.com/file1.zip", "http://www.example.com/file2.pdf" };
        string[] savePathsDifferentDirs = { "C:\\Downloads\\file1.zip", "D:\\Documents\\file2.pdf" };

        MultiFileDownloader.DownloadFiles(fileUrlsDifferentDirs, savePathsDifferentDirs);

        // Download multiple files to the same directory at the same time
        string[] fileUrlsSameDir = { "http://www.example.com/image1.jpg", "http://www.example.com/document.docx" };
        string savePathSameDir = "E:\\Downloads\\";

        string[] savePathsSameDir = new string[fileUrlsSameDir.Length];
        for (int i = 0; i < fileUrlsSameDir.Length; i++)
        {
            savePathsSameDir[i] = Path.Combine(savePathSameDir, Path.GetFileName(new Uri(fileUrlsSameDir[i]).LocalPath));
        }

        MultiFileDownloader.DownloadFiles(fileUrlsSameDir, savePathsSameDir);

        // Download the same file to multiple directories at the same time
        string fileUrlSameFile = "http://www.example.com/data.csv";
        string[] savePathsMultipleDirs = { "F:\\Backup\\data.csv", "G:\\Archive\\data.csv" };

        MultiFileDownloader.DownloadFileToMultipleDirs(fileUrlSameFile, savePathsMultipleDirs);

        Console.WriteLine("All files downloaded successfully.");
    }
}
```
