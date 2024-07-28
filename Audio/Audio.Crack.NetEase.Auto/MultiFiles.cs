namespace Audio.Crack.NetEase.Auto
{
    public class MultiFiles
    {
        public class Sync
        {
            public static void Crack_(params string[] fileNames)
            {
                foreach (var fileName in fileNames)
                {
                    // 执行破解操作，这里假设 CrackAudio 方法是你自己定义的
                    Crack.CrackAudio(fileName);
                }
            }
            // 传入多个文件名称使用方法
            // string[] fileNames = { "audio1.ncm", "audio2.ncm", "audio3.ncm" };
        }

        public class Async
        {
            // var fileNames = new List<string> { "file1.mp3", "file2.mp3", "file3.mp3" };

            public static async Task Crack_(List<string> fileNames)
            {
                var tasks = new List<Task>();

                foreach (var fileName in fileNames)
                {
                    tasks.Add(CrackAudioAsync(fileName));
                }

                await Task.WhenAll(tasks);
            }

            private static async Task CrackAudioAsync(string fileName)
            {
                await Task.Run(() => Crack.CrackAudio(fileName));
            }
        }
    }
}
