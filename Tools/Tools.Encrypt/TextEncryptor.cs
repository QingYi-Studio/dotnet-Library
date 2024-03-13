using System.Text;
using System.Security.Cryptography;

namespace Tools.Encrypt
{
    public class TextEncryptor
    {
        public class Base
        {
            // Base16
            public string EncryptBase16(string text)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                return BitConverter.ToString(bytes).Replace("-", "");
            }

            // Base64
            public string EncryptBase64(string text)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(bytes);
            }
        }

        public class SHA
        {
            // SHA-1
            public string EncryptSHA1(string text)
            {
                using (var sha1 = SHA1.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = sha1.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }

            // SHA-256
            public string EncryptSHA256(string text)
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = sha256.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }

            // SHA-512
            public string EncryptSHA512(string text)
            {
                using (var sha512 = SHA512.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = sha512.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
        }

        public class HMAC
        {
            // HMAC-MD5
            public string EncryptHMACMD5(string text, string key)
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                using (var hmac = new HMACMD5(keyBytes))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = hmac.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }

            // HMAC-SHA-1
            public string EncryptHMACSHA1(string text, string key)
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                using (var hmac = new HMACSHA1(keyBytes))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = hmac.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }

            // HMAC-SHA-256
            public string EncryptHMACSHA256(string text, string key)
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                using (var hmac = new HMACSHA256(keyBytes))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = hmac.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }

            // HMAC-SHA-512
            public string EncryptHMACSHA512(string text, string key)
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                using (var hmac = new HMACSHA512(keyBytes))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = hmac.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
        }        

        public class MD5
        {
            private const uint A = 0x67452301;
            private const uint B = 0xefcdab89;
            private const uint C = 0x98badcfe;
            private const uint D = 0x10325476;

            public string GetMD5Hash(string input)
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                uint[] message = CreateMessage(inputBytes);

                uint a = A;
                uint b = B;
                uint c = C;
                uint d = D;

                for (int i = 0; i < message.Length; i += 16)
                {
                    uint aa = a;
                    uint bb = b;
                    uint cc = c;
                    uint dd = d;

                    // MD5 Round 1
                    a = FF(a, b, c, d, message[i + 0], 7, 0xd76aa478);
                    d = FF(d, a, b, c, message[i + 1], 12, 0xe8c7b756);
                    c = FF(c, d, a, b, message[i + 2], 17, 0x242070db);
                    b = FF(b, c, d, a, message[i + 3], 22, 0xc1bdceee);
                    a = FF(a, b, c, d, message[i + 4], 7, 0xf57c0faf);
                    d = FF(d, a, b, c, message[i + 5], 12, 0x4787c62a);
                    c = FF(c, d, a, b, message[i + 6], 17, 0xa8304613);
                    b = FF(b, c, d, a, message[i + 7], 22, 0xfd469501);
                    a = FF(a, b, c, d, message[i + 8], 7, 0x698098d8);
                    d = FF(d, a, b, c, message[i + 9], 12, 0x8b44f7af);
                    c = FF(c, d, a, b, message[i + 10], 17, 0xffff5bb1);
                    b = FF(b, c, d, a, message[i + 11], 22, 0x895cd7be);
                    a = FF(a, b, c, d, message[i + 12], 7, 0x6b901122);
                    d = FF(d, a, b, c, message[i + 13], 12, 0xfd987193);
                    c = FF(c, d, a, b, message[i + 14], 17, 0xa679438e);
                    b = FF(b, c, d, a, message[i + 15], 22, 0x49b40821);

                    // MD5 Round 2
                    a = GG(a, b, c, d, message[i + 1], 5, 0xf61e2562);
                    d = GG(d, a, b, c, message[i + 6], 9, 0xc040b340);
                    c = GG(c, d, a, b, message[i + 11], 14, 0x265e5a51);
                    b = GG(b, c, d, a, message[i + 0], 20, 0xe9b6c7aa);
                    a = GG(a, b, c, d, message[i + 5], 5, 0xd62f105d);
                    d = GG(d, a, b, c, message[i + 10], 9, 0x2441453);
                    c = GG(c, d, a, b, message[i + 15], 14, 0xd8a1e681);
                    b = GG(b, c, d, a, message[i + 4], 20, 0xe7d3fbc8);
                    a = GG(a, b, c, d, message[i + 9], 5, 0x21e1cde6);
                    d = GG(d, a, b, c, message[i + 14], 9, 0xc33707d6);
                    c = GG(c, d, a, b, message[i + 3], 14, 0xf4d50d87);
                    b = GG(b, c, d, a, message[i + 8], 20, 0x455a14ed);
                    a = GG(a, b, c, d, message[i + 13], 5, 0xa9e3e905);
                    d = GG(d, a, b, c, message[i + 2], 9, 0xfcefa3f8);
                    c = GG(c, d, a, b, message[i + 7], 14, 0x676f02d9);
                    b = GG(b, c, d, a, message[i + 12], 20, 0x8d2a4c8a);

                    // MD5 Round 3
                    a = HH(a, b, c, d, message[i + 5], 4, 0xfffa3942);
                    d = HH(d, a, b, c, message[i + 8], 11, 0x8771f681);
                    c = HH(c, d, a, b, message[i + 11], 16, 0x6d9d6122);
                    b = HH(b, c, d, a, message[i + 14], 23, 0xfde5380c);
                    a = HH(a, b, c, d, message[i + 1], 4, 0xa4beea44);
                    d = HH(d, a, b, c, message[i + 4], 11, 0x4bdecfa9);
                    c = HH(c, d, a, b, message[i + 7], 16, 0xf6bb4b60);
                    b = HH(b, c, d, a, message[i + 10], 23, 0xbebfbc70);
                    a = HH(a, b, c, d, message[i + 13], 4, 0x289b7ec6);
                    d = HH(d, a, b, c, message[i + 0], 11, 0xeaa127fa);
                    c = HH(c, d, a, b, message[i + 3], 16, 0xd4ef3085);
                    b = HH(b, c, d, a, message[i + 6], 23, 0x4881d05);
                    a = HH(a, b, c, d, message[i + 9], 4, 0xd9d4d039);
                    d = HH(d, a, b, c, message[i + 12], 11, 0xe6db99e5);
                    c = HH(c, d, a, b, message[i + 15], 16, 0x1fa27cf8);
                    b = HH(b, c, d, a, message[i + 2], 23, 0xc4ac5665);

                    // MD5 Round 4
                    a = II(a, b, c, d, message[i + 0], 6, 0xf4292244);
                    d = II(d, a, b, c, message[i + 7], 10, 0x432aff97);
                    c = II(c, d, a, b, message[i + 14], 15, 0xab9423a7);
                    b = II(b, c, d, a, message[i + 5], 21, 0xfc93a039);
                    a = II(a, b, c, d, message[i + 12], 6, 0x655b59c3);
                    d = II(d, a, b, c, message[i + 3], 10, 0x8f0ccc92);
                    c = II(c, d, a, b, message[i + 10], 15, 0xffeff47d);
                    b = II(b, c, d, a, message[i + 1], 21, 0x85845dd1);
                    a = II(a, b, c, d, message[i + 8], 6, 0x6fa87e4f);
                    d = II(d, a, b, c, message[i + 15], 10, 0xfe2ce6e0);
                    c = II(c, d, a, b, message[i + 6], 15, 0xa3014314);
                    b = II(b, c, d, a, message[i + 13], 21, 0x4e0811a1);
                    a = II(a, b, c, d, message[i + 4], 6, 0xf7537e82);
                    d = II(d, a, b, c, message[i + 11], 10, 0xbd3af235);
                    c = II(c, d, a, b, message[i + 2], 15, 0x2ad7d2bb);
                    b = II(b, c, d, a, message[i + 9], 21, 0xeb86d391);

                    a += aa;
                    b += bb;
                    c += cc;
                    d += dd;
                }

                byte[] resultBytes = new byte[16];
                BitConverter.GetBytes(a).CopyTo(resultBytes, 0);
                BitConverter.GetBytes(b).CopyTo(resultBytes, 4);
                BitConverter.GetBytes(c).CopyTo(resultBytes, 8);
                BitConverter.GetBytes(d).CopyTo(resultBytes, 12);

                return BitConverter.ToString(resultBytes).Replace("-", "").ToLower();
            }

            private uint RotateLeft(uint x, int n)
            {
                return (x << n) | (x >> (32 - n));
            }

            private uint F(uint x, uint y, uint z)
            {
                return (x & y) | (~x & z);
            }

            private uint G(uint x, uint y, uint z)
            {
                return (x & z) | (y & ~z);
            }

            private uint H(uint x, uint y, uint z)
            {
                return x ^ y ^ z;
            }

            private uint I(uint x, uint y, uint z)
            {
                return y ^ (x | ~z);
            }

            private uint FF(uint a, uint b, uint c, uint d, uint x, int s, uint t)
            {
                return b + RotateLeft((a + F(b, c, d) + x + t), s);
            }

            private uint GG(uint a, uint b, uint c, uint d, uint x, int s, uint t)
            {
                return b + RotateLeft((a + G(b, c, d) + x + t), s);
            }

            private uint HH(uint a, uint b, uint c, uint d, uint x, int s, uint t)
            {
                return b + RotateLeft((a + H(b, c, d) + x + t), s);
            }

            private uint II(uint a, uint b, uint c, uint d, uint x, int s, uint t)
            {
                return b + RotateLeft((a + I(b, c, d) + x + t), s);
            }

            private uint[] CreateMessage(byte[] input)
            {
                int initialLength = input.Length;
                int zeroBytes = (56 - (initialLength + 1) % 64) % 64;
                int messageLength = initialLength + 1 + zeroBytes + 8;
                byte[] messagePadded = new byte[messageLength];

                input.CopyTo(messagePadded, 0);
                messagePadded[initialLength] = 0x80;

                // Append the length of the original message in bits as a 64-bit little-endian integer
                byte[] lengthBytes = BitConverter.GetBytes((ulong)initialLength * 8);
                lengthBytes.CopyTo(messagePadded, messageLength - 8);

                uint[] message = new uint[messageLength / 4];
                for (int i = 0; i < message.Length; i++)
                {
                    message[i] = BitConverter.ToUInt32(messagePadded, i * 4);
                }
                return message;
            }
        }
    }
}
