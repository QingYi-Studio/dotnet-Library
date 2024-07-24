namespace Audio.Crack.NetEase.Auto
{
    public class Folder
    {
        public static void SyncCrack(string folderPath)
        {
            List<string> ncmFiles = GetNcmFiles(folderPath);
            foreach (string file in ncmFiles)
            {
                Crack.CrackAudio(file);
            }
        }

        public static async Task AsyncCrack(string folderPath)
        {
            List<string> ncmFiles = GetNcmFiles(folderPath);
            var tasks = new List<Task>();
            foreach (string file in ncmFiles)
            {
                tasks.Add(Task.Run(() => Crack.CrackAudio(file)));
            }
            await Task.WhenAll(tasks);
        }

        private static List<string> GetNcmFiles(string folderPath)
        {
            List<string> ncmFiles = [];
            string[] files = Directory.GetFiles(folderPath, "*.ncm");
            foreach (string file in files)
            {
                ncmFiles.Add(file);
            }
            return ncmFiles;
        }
    }
}
