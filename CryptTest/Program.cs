using CryptTest.FileCryption;

string inputFile = "test.txt";
string encryptedFile = "encryptedfile.des-encrypt";
string decryptedFile = "decryptedfile.txt";
string key = "12345678"; // 8字节的密钥
string iv = "87654321"; // 8字节的初始向量

CBC.EncryptFile(inputFile, encryptedFile, key, iv);
Console.WriteLine("File encrypted successfully.");

CBC.DecryptFile(encryptedFile, decryptedFile, key, iv);
Console.WriteLine("File decrypted successfully.");
