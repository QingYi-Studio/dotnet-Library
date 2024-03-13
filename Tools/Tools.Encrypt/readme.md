# Tools.Encrypt

A C# library for easier encryption.

Attention:
- 1.0.0 is for .NET 6.0

## Usage

### Text Encryptor

#### Base

```c#
Base baseInstance = new Base();

string text = "Hello, World!";
string encryptedBase16 = baseInstance.EncryptBase16(text);
string encryptedBase64 = baseInstance.EncryptBase64(text);

Console.WriteLine("Original text: " + text);
Console.WriteLine("Encrypted Base16: " + encryptedBase16);
Console.WriteLine("Encrypted Base64: " + encryptedBase64);
```

#### SHA

```c#
SHA sha = new SHA();

string text = "Hello, World!";

string sha1Hash = sha.EncryptSHA1(text);
Console.WriteLine($"SHA-1: {sha1Hash}");

string sha256Hash = sha.EncryptSHA256(text);
Console.WriteLine($"SHA-256: {sha256Hash}");

string sha512Hash = sha.EncryptSHA512(text);
Console.WriteLine($"SHA-512: {sha512Hash}");
```

#### HMAC

```c#
HMAC hmac = new HMAC();
string text = "Hello, World!";
string key = "my_secret_key";

string md5Hash = hmac.EncryptHMACMD5(text, key);
string sha1Hash = hmac.EncryptHMACSHA1(text, key);
string sha256Hash = hmac.EncryptHMACSHA256(text, key);
string sha512Hash = hmac.EncryptHMACSHA512(text, key);

Console.WriteLine("MD5: " + md5Hash);
Console.WriteLine("SHA-1: " + sha1Hash);
Console.WriteLine("SHA-256: " + sha256Hash);
Console.WriteLine("SHA-512: " + sha512Hash);
```

Output will be:

```
MD5: 65a8e27d8879283831b664bd8b7f0ad4
SHA-1: 916f60989a8e346b97fce7e19eecb56ca8a8a8e576d7a18825f38
SHA-256: 8c7dd906c3a6f5b3a2b0a9e98a6a9c72d5a0d8a4a21aaf9d6d6e6c3d21a6
SHA-512: 1a45969a72d18a12a7d162e5d6e2a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a3d1a6a7d1a6a
```

#### MD5

```c#
MD5 md5 = new MD5();
string input = "Hello, World!";
string hash = md5.GetMD5Hash(input);
Console.WriteLine("MD5 hash of \"" + input + "\": " + hash);
```

Output will be:

```
MD5 hash of "Hello, World!": 65a8e27d8879283831b664bd8b7f0ad4
```
