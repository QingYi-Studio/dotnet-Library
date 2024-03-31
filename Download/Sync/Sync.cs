namespace Download.Sync
{
    public class Sync
    {
        public static void DownloadFile(string fileUrl, string savePath)
        /// usage
        /// string fileUrl = "http://www.example.com/file.zip";
        /// string savePath = "C:\\Downloads\\file.zip";
        /// DownloadFile(fileUrl, savePath);
        {
            using HttpClient client = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(fileUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    using (var fileStream = File.Create(savePath))
                    {
                        response.Content.CopyToAsync(fileStream).Wait();
                    }
                    Console.WriteLine("File downloaded successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to download the file. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading file: " + ex.Message);
            }
        }

        public static void DownloadFiles(string[] fileUrls, string[] savePaths)
        {
            if (fileUrls.Length != savePaths.Length)
            {
                throw new ArgumentException("The number of file URLs must be equal to the number of save paths.");
            }

            for (int i = 0; i < fileUrls.Length; i++)
            {
                DownloadFile(fileUrls[i], savePaths[i]);
            }
        }
    }
}
