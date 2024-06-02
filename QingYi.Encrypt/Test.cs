using QingYi.Encrypt.Des;

namespace QingYi.Encrypt
{
    class Test
    {
        static void DesTest()
        {
            string key = "1234";
            DesEncrypt des = new(key);
            des.TextEncrypt("1234");
        }
    }
}
