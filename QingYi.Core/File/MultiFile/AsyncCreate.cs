namespace QingYi.Core.File.MultiFile
{
    public class AsyncCreate
    {
        public static async Task CreateFilesAsync(string[] filePaths)
        {
            try
            {
                Task[] tasks = Array.ConvertAll(filePaths, filePath =>
                {
                    return Task.Run(() =>
                    {
                        Create.CreateFile(filePath);
                    });
                });

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
