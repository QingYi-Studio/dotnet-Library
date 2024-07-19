using System.IO;

namespace QingYi.Core.Folder.Temp
{
    public class CreateFile
    {
        public static string Create(string fileName)
        {
            string filePath = Path.Combine(GetTempFolder.Get(), fileName);

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            return filePath;
        }
    }
}
