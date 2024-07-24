using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QingYi.Cryption.Des.OFB
{
    public class Text
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

        public Text(string key, string iv, string text)
        {
            this.key = key;
            this.iv = iv;
            this.text = text;
        }

        public string Encrypt()
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                // 设置密钥和IV
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

                des.Key = keyBytes;
                des.IV = ivBytes;

                // 设置ECB
                des.Mode = CipherMode.OFB;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                        cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public string Decrypt()
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

                // 设置密钥和IV
                des.Key = keyBytes;
                des.IV = ivBytes;

                // 设置解密算法为DES的ECB模式
                des.Mode = CipherMode.OFB;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] inputBytes = Convert.FromBase64String(text);
                        cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }
    }
}
