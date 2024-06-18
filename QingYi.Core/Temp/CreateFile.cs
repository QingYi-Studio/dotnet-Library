namespace QingYi.Core.Temp
{
    public class CreateFile
    {
        public static void Create(string fileName)
        {
            string filePath = Path.Combine(GetTempFolder.Get(), fileName);

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }
    }
}
