using System.Net;

namespace QingYi.Core.Web
{
    public class GetIP
    {
        /// <summary>
        /// Get IP Address<br></br>
        /// 获取IP地址
        /// </summary>
        /// <param name="url">Url|链接</param>
        /// <returns>IP</returns>
        /// <exception cref="Exception"></exception>
        public static string GetIPAddress(string url)
        {
            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(new Uri(url).Host);
                foreach (IPAddress address in addresses)
                {
                    return address.ToString();
                }
                return "IP not found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get IPv4 Address<br></br>
        /// 获取IPv4地址
        /// </summary>
        /// <param name="url">Url|链接</param>
        /// <returns>IP</returns>
        /// <exception cref="Exception"></exception>
        // 获取IPv4地址
        public static string GetIPv4Address(string url)
        {
            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(new Uri(url).Host);
                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }
                return "IP not found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get IPv6 Address<br></br>
        /// 获取IPv6地址
        /// </summary>
        /// <param name="url">Url|链接</param>
        /// <returns>IP</returns>
        /// <exception cref="Exception"></exception>
        // 获取IPv6地址
        public static string GetIPv6Address(string url)
        {
            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(new Uri(url).Host);
                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        return address.ToString();
                    }
                }
                return "IP not found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
