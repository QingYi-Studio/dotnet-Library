namespace QingYi.Encrypt
{
    public class Encrypt
    {
        public class DesEncrypt(string key, DesEncryptMode mode)
        {
            private string key = key;
            private DesEncryptMode mode = mode;

            public string Key
            {
                get { return key; }
                set { key = value; }
            }

            public DesEncryptMode Mode
            {
                get { return mode; }
                set { mode = value; }
            }

            public void TextEncrypt(string text)
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                switch (mode)
                {
                    case DesEncryptMode.ECB:
                        Des.ECB.Text.Encrypt(text, keyBytes);
                        break;
                    case DesEncryptMode.CBC:
                        Des.CBC.Text.Encrypt(text, keyBytes);
                        break;
                    default:
                        throw new ArgumentException("Invalid encryption mode.");
                }
            }

            public void FileEncrypt(string inputFile, string outputFile)
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);

                switch (mode)
                {
                    case DesEncryptMode.ECB:
                        Des.ECB.File.Encrypt(inputFile, outputFile, keyBytes);
                        break;
                    case DesEncryptMode.CBC:
                        Des.CBC.File.Encrypt(inputFile, outputFile, keyBytes);
                        break;
                    default:
                        throw new ArgumentException("Invalid encryption mode.");
                }
            }
        }

        public enum DesEncryptMode
        {
            ECB,
            CBC
        }
    }
}
