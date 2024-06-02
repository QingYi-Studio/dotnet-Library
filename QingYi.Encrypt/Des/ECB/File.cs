namespace QingYi.Encrypt.Des.ECB
{
    internal class File
    {
        private const int BufferSize = 64 * 1024; // 64KB buffer size for file processing

        // Encrypts a file using DES encryption with ECB mode
        public static void EncryptFile(string inputFile, string outputFile, byte[] key)
        {
            using DES des = DES.Create() ?? throw new NotSupportedException("DES algorithm is not supported on this platform.");
            des.Key = key;
            des.Mode = CipherMode.ECB;

            using FileStream inputStream = new(inputFile, FileMode.Open);
            using FileStream outputStream = new(outputFile, FileMode.Create);
            using CryptoStream cryptoStream = new(outputStream, des.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                cryptoStream.Write(buffer, 0, bytesRead);
            }
        }

        // Decrypts a file using DES encryption with ECB mode
        public static void DecryptFile(string inputFile, string outputFile, byte[] key)
        {
            using DES des = DES.Create() ?? throw new NotSupportedException("DES algorithm is not supported on this platform.");
            des.Key = key;
            des.Mode = CipherMode.ECB;

            using FileStream inputStream = new(inputFile, FileMode.Open);
            using FileStream outputStream = new(outputFile, FileMode.Create);
            using CryptoStream cryptoStream = new(inputStream, des.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
            }
        }
    }
}
