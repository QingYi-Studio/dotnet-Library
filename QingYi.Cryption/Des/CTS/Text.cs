using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QingYi.Cryption.Des.CTS
{
    public class Text
    {
        private readonly byte[] key;
        private readonly string inputString;

        public Text(byte[] key, string inputString)
        {
            this.key = key;
            this.inputString = inputString;
        }

        public string EncryptString()
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(inputString);

            using (DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider())
            {
                desCryptoProvider.Key = key;
                desCryptoProvider.Mode = CipherMode.CTS; // 使用CTS模式

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public string DecryptString()
        {
            byte[] encryptedBytes = Convert.FromBase64String(inputString);

            using (DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider())
            {
                desCryptoProvider.Key = key;
                desCryptoProvider.Mode = CipherMode.CTS; // 使用CTS模式

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoProvider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }
    }
}
