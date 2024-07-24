using System.Security.Cryptography;
using System.Text;

namespace CryptTest.TextCryption
{
    internal class CBC
    {
        public static string DESEncrypt(string plainText, string key, string iv)
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
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

        public static string DESDecrypt(string encryptedText, string key, string iv)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
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
