using CryptTest.TextCryption;

string key = "12345678"; // 8字节的密钥
string iv = "87654321"; // 8字节的初始向量

Console.Write("Enter plain text to encrypt: ");
string plainText = Console.ReadLine()!;

string encryptedText = CBC.DESEncrypt(plainText!, key, iv);
Console.WriteLine("Encrypted Text: " + encryptedText);

string decryptedText = CBC.DESDecrypt(encryptedText, key, iv);
Console.WriteLine("Decrypted Text: " + decryptedText);

