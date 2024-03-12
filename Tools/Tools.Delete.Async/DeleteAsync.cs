public static class AsyncDelete
{
    public static async Task DeleteFilesAndFoldersAsync(string[] paths, bool outputMessages = false)
    {
        await Task.Run(() =>
        {
            Delete.DeleteFilesAndFolders(paths, outputMessages);
        });
    }

    public static async Task ForceDeleteAsync(string[] paths, bool outputMessages = false)
    {
        await Task.Run(() =>
        {
            Delete.ForceDelete(paths, outputMessages);
        });
    }
}
