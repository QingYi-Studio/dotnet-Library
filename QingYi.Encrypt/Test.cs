using QingYi.Encrypt.Des;

namespace QingYi.Encrypt
{
    class Test
    {
        static void DesTest()
        {
            string key = "1234";
            DesEncrypt des = new DesEncrypt(key, Mode.ECB);
            des.TextEncrypt("1234");

        }
    }
}
