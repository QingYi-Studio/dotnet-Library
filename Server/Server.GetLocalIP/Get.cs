using System.Net.Sockets;
using System.Net;

namespace Server.GetLocalIP
{
    public class Get
    {
        public static IPAddress? GetLocalIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress ipAddress in ipAddresses)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress;
                }
            }

            return null;
        }
    }
}
