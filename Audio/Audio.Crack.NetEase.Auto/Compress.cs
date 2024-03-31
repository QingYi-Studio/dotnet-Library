using System.IO.Compression;
using SevenZip;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;

namespace Audio.Crack.NetEase.Auto
{
    public class Compress
    {
        public class Zip
        {
            public class Sync
            {
                public static void CrackAndPack(string inputZipPath, string outputZipPath)
                {
                    using ZipArchive inputArchive = ZipFile.OpenRead(inputZipPath);
                    using ZipArchive outputArchive = ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    foreach (ZipArchiveEntry entry in inputArchive.Entries)
                    {
                        if (entry.FullName.EndsWith(".ncm", System.StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.ExtractToFile(tempFilePath, true);
                            Crack.CrackAudio(tempFilePath);
                            AddToArchive(outputArchive, tempFilePath, entry.Name);
                            File.Delete(tempFilePath);
                        }
                    }
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }

            public class Async
            {
                public static async Task CrackAndPackAsync(string inputZipPath, string outputZipPath)
                {
                    using ZipArchive inputArchive = ZipFile.OpenRead(inputZipPath);
                    using ZipArchive outputArchive = ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    List<Task> crackingTasks = [];

                    foreach (ZipArchiveEntry entry in inputArchive.Entries)
                    {
                        if (entry.FullName.EndsWith(".ncm", System.StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.ExtractToFile(tempFilePath, true);
                            Task crackingTask = CrackAudioAsync(tempFilePath);
                            crackingTasks.Add(crackingTask);
                            AddToArchive(outputArchive, tempFilePath, entry.Name);
                        }
                    }

                    await Task.WhenAll(crackingTasks);
                }

                private static async Task CrackAudioAsync(string fileName)
                {
                    await Task.Run(() =>
                    {
                        Crack.CrackAudio(fileName);
                    });
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }
        }

        public class Rar
        {
            public class Sync
            {
                public static void CrackAndPack(string inputRarPath, string outputZipPath)
                {
                    using var archive = RarArchive.Open(inputRarPath);
                    using ZipArchive outputArchive = ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.Key.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.WriteToFile(tempFilePath);
                            Crack.CrackAudio(tempFilePath);
                            AddToArchive(outputArchive, tempFilePath, entry.Key);
                            File.Delete(tempFilePath);
                        }
                    }
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }

            public class Async
            {
                public static async Task CrackAndPackAsync(string inputRarPath, string outputZipPath)
                {
                    using var archive = RarArchive.Open(inputRarPath);
                    using ZipArchive outputArchive = ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    List<Task> crackingTasks = [];

                    foreach (var entry in archive.Entries)
                    {
                        if (entry.Key.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            entry.WriteToFile(tempFilePath);
                            Task crackingTask = CrackAudioAsync(tempFilePath);
                            crackingTasks.Add(crackingTask);
                            AddToArchive(outputArchive, tempFilePath, entry.Key);
                        }
                    }

                    await Task.WhenAll(crackingTasks);
                }

                private static async Task CrackAudioAsync(string fileName)
                {
                    await Task.Run(() =>
                    {
                        Crack.CrackAudio(fileName);
                    });
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }
        }

        public class SevenZip_
        {
            public class Sync
            {
                public static void CrackAndPack(string input7zPath, string outputZipPath)
                {
                    using SevenZipExtractor extractor = new(input7zPath);
                    using ZipArchive outputArchive = ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    foreach (var entry in extractor.ArchiveFileData)
                    {
                        if (entry.FileName.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            using (FileStream fs = new(tempFilePath, FileMode.Create))
                            {
                                extractor.ExtractFile(entry.Index, fs);
                            }
                            Crack.CrackAudio(tempFilePath);
                            AddToArchive(outputArchive, tempFilePath, entry.FileName);
                            File.Delete(tempFilePath);
                        }
                    }
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }

            public class Async
            {
                public static async Task CrackAndPackAsync(string input7zPath, string outputZipPath)
                {
                    using SevenZipExtractor extractor = new(input7zPath);
                    using ZipArchive outputArchive = ZipFile.Open(outputZipPath, ZipArchiveMode.Create);
                    List<Task> crackingTasks = [];

                    foreach (var entry in extractor.ArchiveFileData)
                    {
                        if (entry.FileName.EndsWith(".ncm", StringComparison.OrdinalIgnoreCase))
                        {
                            string tempFilePath = Path.GetTempFileName();
                            using (FileStream fs = new(tempFilePath, FileMode.Create))
                            {
                                extractor.ExtractFile(entry.Index, fs);
                            }
                            Task crackingTask = CrackAudioAsync(tempFilePath);
                            crackingTasks.Add(crackingTask);
                            AddToArchive(outputArchive, tempFilePath, entry.FileName);
                        }
                    }

                    await Task.WhenAll(crackingTasks);
                }

                private static async Task CrackAudioAsync(string fileName)
                {
                    await Task.Run(() =>
                    {
                        Crack.CrackAudio(fileName);
                    });
                }

                private static void AddToArchive(ZipArchive archive, string filePath, string entryName)
                {
                    archive.CreateEntryFromFile(filePath, entryName);
                }
            }
        }

        public class GZip
        {
            public static void SyncCrack(string gzFilePath, string exportFileName)
            {
                var tempDir = ExtractGz(gzFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");
                foreach (var ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }
                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            public static async Task AsyncCrack(string gzFilePath, string exportFileName)
            {
                var tempDir = ExtractGz(gzFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");
                var tasks = new List<Task>();
                foreach (var ncmFile in ncmFiles)
                {
                    tasks.Add(Task.Run(() => Crack.CrackAudio(ncmFile)));
                }
                await Task.WhenAll(tasks);
                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            private static string ExtractGz(string gzFilePath)
            {
                var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);
                using (var fileStream = File.OpenRead(gzFilePath))
                using (var gzipStream = new GZipStream(fileStream, System.IO.Compression.CompressionMode.Decompress))
                {
                    using var archive = new ZipArchive(gzipStream, ZipArchiveMode.Read);
                    archive.ExtractToDirectory(tempDir);
                }
                return tempDir;
            }

            private static void CompressFiles(IEnumerable<string> fileNames, string exportFileName)
            {
                using var zipStream = new FileStream(exportFileName, FileMode.Create);
                using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create);
                foreach (var fileName in fileNames)
                {
                    var entry = archive.CreateEntry(Path.GetFileName(fileName));
                    using var entryStream = entry.Open();
                    using var fileStream = new FileStream(fileName, FileMode.Open);
                    fileStream.CopyTo(entryStream);
                }
            }
        }

        public class Tar()
        {
            public static void SyncCrackAndCompress(string tarFilePath, string exportFileName)
            {
                var tempDir = ExtractTar(tarFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");

                foreach (var ncmFile in ncmFiles)
                {
                    Crack.CrackAudio(ncmFile);
                }

                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            public static async Task AsyncCrackAndCompress(string tarFilePath, string exportFileName)
            {
                var tempDir = ExtractTar(tarFilePath);
                var ncmFiles = Directory.GetFiles(tempDir, "*.ncm");

                await Task.WhenAll(Array.ConvertAll(ncmFiles, async ncmFile =>
                {
                    await Task.Run(() => Crack.CrackAudio(ncmFile));
                }));

                CompressFiles(ncmFiles, exportFileName);
                Directory.Delete(tempDir, true);
            }

            private static string ExtractTar(string tarFilePath)
            {
                var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);
                ZipFile.ExtractToDirectory(tarFilePath, tempDir);
                return tempDir;
            }

            private static void CompressFiles(IEnumerable<string> fileNames, string exportFileName)
            {
                using var zipStream = new FileStream(exportFileName, FileMode.Create);
                using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create);
                foreach (var fileName in fileNames)
                {
                    var entry = archive.CreateEntry(Path.GetFileName(fileName));
                    using var entryStream = entry.Open();
                    using var fileStream = new FileStream(fileName, FileMode.Open);
                    fileStream.CopyTo(entryStream);
                }
            }
        }
    }
}
