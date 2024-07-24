using System.IO;
using System.Security.Cryptography;

namespace QingYi.Cryption.Des.CTS
{
    public class File
    {
        /// <summary>
        /// The key of the file you want to encrypt/decrypt
        /// </summary>
        private readonly byte[] key;

        /// <summary>
        /// The path of the file you want to encrypt/decrypt
        /// </summary>
        private readonly string inputFile;

        /// <summary>
        /// The output path of the file you want to encrypt/decrypt
        /// </summary>
        private readonly string outputFile;

        public File(byte[] key, string inputFile, string outputFile)
        {
            this.key = key;
            this.inputFile = inputFile;
            this.outputFile = outputFile;
        }

        public void Encrypt()
        {
            using (DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider())
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                desCryptoProvider.Key = key;
                desCryptoProvider.Mode = CipherMode.CTS; // 使用CTS模式

                using (CryptoStream cryptoStream = new CryptoStream(fsOutput, desCryptoProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public void Decrypt()
        {
            using (DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider())
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                desCryptoProvider.Key = key;
                desCryptoProvider.Mode = CipherMode.CTS; // 使用CTS模式

                using (CryptoStream cryptoStream = new CryptoStream(fsOutput, desCryptoProvider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}
