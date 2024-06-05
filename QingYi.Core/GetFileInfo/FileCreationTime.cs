using System;

namespace QingYi.Core.GetFileInfo
{
    public class FileCreationTime
    {
        public static DateTime Get(string filePath)
        {
            Select select = new Select();

            var result = select.SelectFile(filePath);

            DateTime dateTime = result.Item4;

            return dateTime;
        }
    }
}
