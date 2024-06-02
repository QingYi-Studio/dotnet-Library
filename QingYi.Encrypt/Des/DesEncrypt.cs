namespace QingYi.Encrypt.Des
{
    public class DesEncrypt(string key, Mode mode)
    {
        private string key = key;
        private Mode mode = mode;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public Mode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public void TextEncrypt(string text)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            switch (mode)
            {
                case Mode.ECB:
                    ECB.Text.Encrypt(text, keyBytes);
                    break;
                case Mode.CBC:
                    CBC.Text.Encrypt(text, keyBytes);
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
                case Mode.ECB:
                    ECB.File.Encrypt(inputFile, outputFile, keyBytes);
                    break;
                case Mode.CBC:
                    CBC.File.Encrypt(inputFile, outputFile, keyBytes);
                    break;
                default:
                    throw new ArgumentException("Invalid encryption mode.");
            }
        }
    }

    public enum Mode
    {
        ECB,
        CBC
    }
}
