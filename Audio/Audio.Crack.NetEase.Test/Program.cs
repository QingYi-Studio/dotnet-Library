using Audio.Crack.NetEase.Auto;

SingleFile.Crack_("input.ncm");

static void multi()
{
    static void multisync()
    {
        string[] fileNames = ["audio1.ncm", "audio2.ncm", "audio3.ncm"];
        MultiFiles.Sync.Crack_(fileNames);
    }

    static async Task multiasyncAsync()
    {
        var fileNames = new List<string> { "file1.mp3", "file2.mp3", "file3.mp3" };
        await MultiFiles.Async.Crack_(fileNames);
    }
}

static void folder()
{
    static void syncfolder()
    {
        // 指定你的 .ncm 文件所在的文件夹路径
        string folderPath = @"D:\Music\NcmFiles";
        Folder.SyncCrack(folderPath);
    }

    static async void asyncfolder()
    {
        // 指定你的 .ncm 文件所在的文件夹路径
        string folderPath = @"D:\Music\NcmFiles";
        await Folder.AsyncCrack(folderPath);
    }
}

static void compress()
{
    static void zip()
    {
        static void sync()
        {
            Compress.Zip.Sync.CrackAndPack("input.zip", "output_sync.zip");
        }

        static async void async()
        {
            await Compress.Zip.Async.CrackAndPackAsync("input.zip", "output_async.zip");
        }
    }
    
    static void rar()
    {
        static void sync()
        {
            Compress.Rar.Sync.CrackAndPack("input.rar", "output_sync.zip");
        }

        static async void async()
        {
            await Compress.Rar.Async.CrackAndPackAsync("input.rar", "output_sync.zip");
        }
    }

    static void seven_zip()
    {
        static void sync()
        {
            Compress.SevenZip_.Sync.CrackAndPack("input.7z", "output_sync.zip");
        }

        static async void async()
        {
            await Compress.SevenZip_.Async.CrackAndPackAsync("input.7z", "output_async.zip");
        }
    }

    static void gzip()
    {
        static void sync()
        {
            Compress.GZip.SyncCrack("your_gz_file.gz", "exported.zip");
        }

        static async void async()
        {
            await Compress.GZip.AsyncCrack("your_gz_file.gz", "exported.zip");
        }
    }

    static void tar()
    {
        string tarFilePath = "your_tar_file.tar";
        string exportFileName = "exported.zip";

        // Sync
        Compress.Tar.SyncCrackAndCompress(tarFilePath, exportFileName);

        // Async
        Compress.Tar.AsyncCrackAndCompress(tarFilePath, exportFileName).Wait();
    }

    static void tar_gzip()
    {

    }

    static void bzip()
    {

    }

    static void bzip2()
    {

    }

    static void tar_bzip2()
    {

    }

    static void z()
    {

    }
}
