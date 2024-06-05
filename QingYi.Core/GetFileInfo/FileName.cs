namespace QingYi.Core.GetFileInfo
{
    public class FileName
    {
        public static string Get(string filePath)
        {
            Select select = new Select();

            var result = select.SelectFile(filePath);

            string fileName = result.Item1;

            return fileName;
        }
    }
}
