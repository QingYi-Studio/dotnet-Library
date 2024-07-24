using System.Security.Cryptography;
using System.Text;

namespace CryptTest.FileCryption
{
    internal class CBC
    {
        // 加密文件
        public static void EncryptFile(string inputFile, string outputFile, string key, string iv)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (DES desAlg = DES.Create())
            {
                desAlg.Mode = CipherMode.CBC;
                desAlg.Key = keyBytes;
                desAlg.IV = ivBytes;

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(fsOutput, desAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        int data;
                        while ((data = fsInput.ReadByte()) != -1)
                        {
                            csEncrypt.WriteByte((byte)data);
                        }
                    }
                }
            }
        }

        // 解密文件
        public static void DecryptFile(string inputFile, string outputFile, string key, string iv)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (DES desAlg = DES.Create())
            {
                desAlg.Mode = CipherMode.CBC;
                desAlg.Key = keyBytes;
                desAlg.IV = ivBytes;

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(fsInput, desAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        int data;
                        while ((data = csDecrypt.ReadByte()) != -1)
                        {
                            fsOutput.WriteByte((byte)data);
                        }
                    }
                }
            }
        }
    }
}
