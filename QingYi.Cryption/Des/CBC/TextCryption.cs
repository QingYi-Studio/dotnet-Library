using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QingYi.Cryption.Des.CBC
{
    public class TextCryption
    {
        /// <summary>
        /// The key of the text you want to encrypt/decrypt
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The iv of the text you want to encrypt/decrypt
        /// </summary>
        private readonly string iv;

        /// <summary>
        /// The text content you want encrypted
        /// </summary>
        private readonly string text;

        public TextCryption(string key, string iv, string text)
        {
            this.key = key;
            this.iv = iv;
            this.text = text;
        }

        public string Encrypt()
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(text);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (DES desAlg = DES.Create())
            {
                desAlg.Mode = CipherMode.CBC;
                desAlg.Key = keyBytes;
                desAlg.IV = ivBytes;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, desAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plaintextBytes, 0, plaintextBytes.Length);
                        csEncrypt.FlushFinalBlock();
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public string Decrypt()
        {
            byte[] cipherTextBytes = Convert.FromBase64String(text);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (DES desAlg = DES.Create())
            {
                desAlg.Mode = CipherMode.CBC;
                desAlg.Key = keyBytes;
                desAlg.IV = ivBytes;

                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, desAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
