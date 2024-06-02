namespace QingYi.Encrypt.Des
{
    public class DesEncrypt(string key)
    {
        public void TextEncrypt(string text)
        {
            // 将密钥转换为字节数组
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key.IfKeyEmpty(key));

            // 调用ECB.Text.Encrypt方法
            ECB.Text.Encrypt(text, keyBytes);
        }

        public void FileEncrypt(string inputFile, string outputFile)
        {
            // 将密钥转换为字节数组
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key.IfKeyEmpty(key));

            ECB.File.EncryptFile(inputFile, outputFile, keyBytes);
        }
    }
}
