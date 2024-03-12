namespace Tools.HTTP
{
    public class HttpPostFileUploader
    {
        public async Task<string> UploadFile(string url, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.", filePath);

            using (var client = new HttpClient())
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileName = Path.GetFileName(filePath);
                        var fileContent = new StreamContent(fileStream);
                        content.Add(fileContent, "file", fileName);

                        var response = await client.PostAsync(url, content);
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}
