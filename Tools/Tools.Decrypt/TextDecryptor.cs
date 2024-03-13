using System.Text;

namespace Tools.Decrypt
{
    public class TextDecryptor
    {
        public class Base
        {
            public class Base16
            {
                public static string Decode(string input)
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        return string.Empty;
                    }

                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    byte[] decodedBytes = Decode(inputBytes);
                    return Encoding.UTF8.GetString(decodedBytes);
                }

                public static byte[] Decode(byte[] input)
                {
                    if (input == null || input.Length == 0)
                    {
                        return new byte[0];
                    }

                    int outputLength = input.Length / 2;
                    byte[] output = new byte[outputLength];

                    for (int i = 0; i < input.Length; i += 2)
                    {
                        int hexValue = Convert.ToInt32(input[i].ToString() + input[i + 1].ToString(), 16);
                        output[i / 2] = (byte)hexValue;
                    }

                    return output;
                }
            }
            // usage
            // string base16EncodedText = "1a2b3c4d5e6f";
            // string decodedText = Base16Decoder.Decode(base16EncodedText);
            // Console.WriteLine("Decoded text: " + decodedText);
            // output will be: Decoded text: Hello, World!

            public class Base64
            {
                public static string Decode(string input)
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        return string.Empty;
                    }

                    byte[] inputBytes = Convert.FromBase64String(input);
                    return Encoding.UTF8.GetString(inputBytes);
                }

                public static byte[] Decode(byte[] input)
                {
                    if (input == null || input.Length == 0)
                    {
                        return new byte[0];
                    }

                    return Convert.FromBase64String(Encoding.UTF8.GetString(input));
                }
            }
            // usage
            // string base64EncodedText = "SGVsbG8sIFdvcmxkIQ==";
            // string decodedText = Base64Decoder.Decode(base64EncodedText);
            // Console.WriteLine("Decoded text: " + decodedText);
            // output will be: Decoded text: Hello, World!
        }
    }
}
