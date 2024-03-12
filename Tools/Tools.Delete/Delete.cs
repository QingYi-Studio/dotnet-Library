using System;
using System.IO;

public static class Delete
{
    public static void DeleteFilesAndFolders(string[] paths, bool outputMessages = false)
    {
        foreach (var path in paths)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                    if (outputMessages)
                    {
                        Console.WriteLine($"Deleted folder: {path}");
                    }
                }
                catch (Exception ex)
                {
                    if (outputMessages)
                    {
                        Console.WriteLine($"Failed to delete folder {path}. Error: {ex.Message}");
                    }
                }
            }
            else if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    if (outputMessages)
                    {
                        Console.WriteLine($"Deleted file: {path}");
                    }
                }
                catch (Exception ex)
                {
                    if (outputMessages)
                    {
                        Console.WriteLine($"Failed to delete file {path}. Error: {ex.Message}");
                    }
                }
            }
            else if (path.Contains("*"))
            {
                var directory = Path.GetDirectoryName(path);
                var pattern = Path.GetFileName(path);

                if (Directory.Exists(directory))
                {
                    var files = Directory.GetFiles(directory, pattern);

                    foreach (var file in files)
                    {
                        try
                        {
                            File.Delete(file);
                            if (outputMessages)
                            {
                                Console.WriteLine($"Deleted file: {file}");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (outputMessages)
                            {
                                Console.WriteLine($"Failed to delete file {file}. Error: {ex.Message}");
                            }
                        }
                    }
                }
            }
            else
            {
                if (outputMessages)
                {
                    Console.WriteLine($"Invalid path: {path}");
                }
            }
        }
    }

    public static void ForceDelete(string[] paths, bool outputMessages = false)
    {
        foreach (var path in paths)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    if (outputMessages)
                    {
                        Console.WriteLine($"Force deleted folder: {path}");
                    }
                }
                else if (File.Exists(path))
                {
                    File.Delete(path);
                    if (outputMessages)
                    {
                        Console.WriteLine($"Force deleted file: {path}");
                    }
                }
                else
                {
                    if (outputMessages)
                    {
                        Console.WriteLine($"File or folder does not exist: {path}");
                    }
                }
            }
            catch (Exception ex)
            {
                if (outputMessages)
                {
                    Console.WriteLine($"Failed to force delete {path}. Error: {ex.Message}");
                }
            }
        }
    }
}
