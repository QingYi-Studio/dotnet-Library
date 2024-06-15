namespace QingYi.Encrypt.Des.ECB
{
    internal class Text
    {
        // 使用DES算法和ECB模式对文本进行加密
        public static string Encrypt(string plainText, byte[] key)
        {
            byte[] encryptedBytes;

            using (var des = DES.Create())
            {
                des.Key = key;
                des.Mode = CipherMode.ECB; // 使用ECB模式

                using var memoryStream = new MemoryStream();
                using (var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using var streamWriter = new StreamWriter(cryptoStream);
                    streamWriter.Write(plainText);
                }

                encryptedBytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        // 使用DES算法和ECB模式对文本进行解密
        public static string Decrypt(string encryptedText, byte[] key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            string decryptedText;

            using (var des = DES.Create())
            {
                des.Key = key;
                des.Mode = CipherMode.ECB; // 使用ECB模式

                using var memoryStream = new MemoryStream(encryptedBytes);
                using var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);
                decryptedText = streamReader.ReadToEnd();
            }

            return decryptedText;
        }
    }
}
