namespace Audio.Convert
{
    // 用于读取大端字节序的扩展方法
    public static class BinaryReaderExtensions
    {
        public static int ReadInt32BigEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
