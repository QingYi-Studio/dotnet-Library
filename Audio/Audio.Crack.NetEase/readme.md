# Audio.Crack.NetEase

A GitHub based on many open source projects and you give the solution to produce a simple NetEase cloud music download down NCM files into MP3 files.

Of course, the premise is that there must be VIP, not cracked VIP so that you can download without VIP.

I'll never support old .NET versions.

## Usage

```c#
string inputFilePath = "input.ncm";
string outputFilePath = "output.mp3";
NcmDecryptor.Decrypt(inputFilePath, outputFilePath);
```

## NCM file structure

| 信息| 大小| 说明|
|-------|------|------|
|Magic Header|10 bytes|文件头|
|Key Length|4 bytes|AES128加密后的RC4密钥长度，字节是按小端排序。|
|Key Data|Key Length|用AES128加密后的RC4密钥。<br/>1. 先按字节对0x64进行异或。<br/>2. AES解密,去除填充部分。<br/>3. 去除最前面'neteasecloudmusic'17个字节，得到RC4密钥。|
|Music Info Length|4 bytes|音乐相关信息的长度，小端排序。|
|Music Info Data|Music Info Length|Json格式音乐信息数据。</br>1. 按字节对0x63进行异或。<br/>2. 去除最前面22个字节。<br/>3. Base64进行解码。<br/>4. AES解密。<br/>6. 去除前面6个字节得到Json数据。|
|CRC|4 bytes|跳过|
|Gap|5 bytes|跳过|
|Image Size|4 bytes|图片的大小|
|Image|Image Size|图片数据|
|Music Data| - |1. RC4-KSA生成S盒。<br/>2. 用S盒解密（自定义的解密方法)，不是RC4-PRGA解密。|

两个AES对应密钥
`unsigned char meta_key[] = { 0x23,0x31,0x34,0x6C,0x6A,0x6B,0x5F,0x21,0x5C,0x5D,0x26,0x30,0x55,0x3C,0x27,0x28 };`
`unsigned char core_key[] = { 0x68,0x7A,0x48,0x52,0x41,0x6D,0x73,0x6F,0x35,0x6B,0x49,0x6E,0x62,0x61,0x78,0x57 };`
