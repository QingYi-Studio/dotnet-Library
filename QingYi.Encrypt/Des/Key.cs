namespace QingYi.Encrypt.Des
{
    internal class Key
    {
        public static string IfKeyEmpty(string? key)
        {
            if (key != null)
            {
                return key;
            }
            return "0000";
        }
    }
}
