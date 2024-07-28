namespace Audio.Crack.NetEase
{
    public class Crack
    {
        public static void CrackAudio(string filePath)
        {
            // 创建 NeteaseCrypt 类的实例
            Crypt neteaseCrypt = new(filePath);

            // 启动转换过程
            int result = neteaseCrypt.Dump();

            // 修复元数据
            neteaseCrypt.FixMetadata();

            // [务必]销毁 NeteaseCrypt 类的实例
            neteaseCrypt.Destroy();
        }
    }
}
