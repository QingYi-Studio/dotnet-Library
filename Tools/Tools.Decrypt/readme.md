# Tools.Decrypt

A C# library for easier decryption.

Attention:
- 1.0.0 is for .NET 6.0
- 1.1.0 and later are for .NET 8.0

## Usage

### Text Decryptor

#### Base 16

```c#
string input = "1a2b3c4d";
byte[] inputBytes = Encoding.UTF8.GetBytes(input);
byte[] decodedBytes = Base16.Decode(inputBytes);
string decodedString = Encoding.UTF8.GetString(decodedBytes);

Console.WriteLine("Input: " + input);
Console.WriteLine("Decoded: " + decodedString);
```

#### Base 64

```c#
string input = "SGVsbG8gV29ybGQh";
byte[] inputBytes = Encoding.UTF8.GetBytes(input);
byte[] decodedBytes = Base64.Decode(inputBytes);
string decodedString = Encoding.UTF8.GetString(decodedBytes);

Console.WriteLine("Input: " + input);
Console.WriteLine("Decoded: " + decodedString);
```
