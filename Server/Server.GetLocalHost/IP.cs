using System.Net;

namespace Server.GetLocalHost
{
    public class IP
    {
        public static string GetIP()
        {
            string LocalIP;
            LocalIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            return LocalIP;
        }
    }
}
