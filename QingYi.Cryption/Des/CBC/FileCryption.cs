using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QingYi.Cryption.Des.CBC
{
    public class FileCryption
    {
        /// <summary>
        /// The key of the file you want to encrypt/decrypt
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The iv of the file you want to encrypt/decrypt
        /// </summary>
        private readonly string iv;

        /// <summary>
        /// The path of the file you want to encrypt/decrypt
        /// </summary>
        private readonly string inputFile;

        /// <summary>
        /// The output path of the file you want to encrypt/decrypt
        /// </summary>
        private readonly string outputFile;

        public FileCryption(string key, string iv, string inputFile, string outputFile)
        {
            this.key = key;
            this.iv = iv;
            this.inputFile = inputFile;
            this.outputFile = outputFile;
        }

        public void Encrypt()
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

        public void Decrypt()
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
