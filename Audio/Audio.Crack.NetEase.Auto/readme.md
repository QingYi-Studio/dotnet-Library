# Audio.Crack.NetEase.Auto

A simple package that can crack the NetEase VIP audio easily.

I've added a lot of powerful features to suit a wide variety of situations.

## Usage

### Single File

```c#
SingleFile.Crack_("input.ncm");
```

### Multi Files

#### Sync

```c#
static void multisync()
{
    string[] fileNames = ["audio1.ncm", "audio2.ncm", "audio3.ncm"];
    MultiFiles.Sync.Crack_(fileNames);
}
```

#### Async

```c#
static async Task multiasyncAsync()
{
    var fileNames = new List<string> { "file1.mp3", "file2.mp3", "file3.mp3" };
    await MultiFiles.Async.Crack_(fileNames);
}
```

### Folder

#### Sync

```c#
static void syncfolder()
{
    string folderPath = @"D:\Music\NcmFiles";
    Folder.SyncCrack(folderPath);
}
```

#### Async

```c#
static async void asyncfolder()
{
    string folderPath = @"D:\Music\NcmFiles";
    await Folder.AsyncCrack(folderPath);
}
```

### Compress

#### Zip

```c#
static void sync()
{
    Compress.Zip.Sync.CrackAndPack("input.zip", "output_sync.zip");
}

static async void async()
{
    await Compress.Zip.Async.CrackAndPackAsync("input.zip", "output_async.zip");
}
```

#### RAR

```c#
static void sync()
{
    Compress.Rar.Sync.CrackAndPack("input.rar", "output_sync.zip");
}

static async void async()
{
    await Compress.Rar.Async.CrackAndPackAsync("input.rar", "output_sync.zip");
}
```

#### 7-Zip

```c#
static void sync()
{
    Compress.SevenZip_.Sync.CrackAndPack("input.7z", "output_sync.zip");
}

static async void async()
{
    await Compress.SevenZip_.Async.CrackAndPackAsync("input.7z", "output_async.zip");
}
```

#### G-Zip

```c#
static void sync()
{
    Compress.GZip.SyncCrack("your_gz_file.gz", "exported.zip");
}

static async void async()
{
    await Compress.GZip.AsyncCrack("your_gz_file.gz", "exported.zip");
}
```

#### Tar

```c#
string tarFilePath = "your_tar_file.tar";
string exportFileName = "exported.zip";

// Sync
Compress.Tar.SyncCrackAndCompress(tarFilePath, exportFileName);

// Async
Compress.Tar.AsyncCrackAndCompress(tarFilePath, exportFileName).Wait();
```
