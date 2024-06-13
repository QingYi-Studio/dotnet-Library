namespace QingYi.Core.GetFileInfo
{
    public class FileSize
    {
        public static string Get(string filePath)
        {
            Select select = new Select();

            var result = select.SelectFile(filePath);

            string fileSize = result.Item3;

            return fileSize;
        }
    }
}
