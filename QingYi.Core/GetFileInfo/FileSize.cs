namespace QingYi.Core.GetFileInfo
{
    public class FileSize
    {
        public static long Get(string filePath)
        {
            Select select = new Select();

            var result = select.SelectFile(filePath);

            long fileSize = result.Item3;

            return fileSize;
        }
    }
}
