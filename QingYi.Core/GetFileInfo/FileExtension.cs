namespace QingYi.Core.GetFileInfo
{
    public class FileExtension
    {
        public static string Get(string filePath)
        {
            Select select = new Select();

            var result = select.SelectFile(filePath);

            string fileExtension = result.Item2;

            return fileExtension;
        }
    }
}
