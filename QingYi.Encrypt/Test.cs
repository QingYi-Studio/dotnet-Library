namespace QingYi.Encrypt
{
    class Test
    {
        static void DesTest()
        {
            string key = "1234";
            Encrypt.DesEncrypt desEcb = new(key, Encrypt.DesEncryptMode.ECB);
            Encrypt.DesEncrypt desCbc = new(key, Encrypt.DesEncryptMode.CBC);
            desEcb.TextEncrypt("1234");
            desCbc.TextEncrypt("1234");
        }
    }
}
