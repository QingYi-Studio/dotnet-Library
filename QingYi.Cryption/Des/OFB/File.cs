using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QingYi.Cryption.Des.OFB
{
    public class File
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

        public File(string key, string iv, string inputFile, string outputFile)
        {
            this.key = key;
            this.iv = iv;
            this.inputFile = inputFile;
            this.outputFile = outputFile;
        }

        public void Encrypt()
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                // 设置key与iv
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                des.Key = keyBytes;
                des.IV = ivBytes;

                // 设置ECB模式
                des.Mode = CipherMode.OFB;

                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    // 创建加密流
                    using (ICryptoTransform encryptor = des.CreateEncryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;

                        // 从输入文件读取数据并加密后写入输出文件
                        while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            cryptoStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }

        public void Decrypt()
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(this.key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(this.iv);

                // 设置密钥和IV
                des.Key = keyBytes;
                des.IV = ivBytes;

                // 设置解密算法为DES的ECB模式
                des.Mode = CipherMode.OFB;

                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    // 创建解密流
                    using (ICryptoTransform decryptor = des.CreateDecryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;

                        // 从加密文件读取数据并解密后写入输出文件
                        while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outputFileStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }
    }
}
