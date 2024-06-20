using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace QingYi.Core.Web
{
    public class GetIP
    {
        /// <summary>
        /// Get IPv4 Address<br></br>
        /// 获取IPv4地址
        /// </summary>
        /// <param name="url">Url|链接</param>
        /// <returns>IP</returns>
        /// <exception cref="Exception"></exception>
        public static IPAddress GetIPv4Address(string url)
        {
            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(url);

                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return address; // 返回找到的第一个 IPv4 地址
                    }
                }

                // 如果未找到符合条件的 IPv4 地址
                throw new Exception("No matching IPv4 address was found. Procedure");
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
        public static IPAddress[] GetIPv6Addresses(string url)
        {
            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(url);
                IPAddress[] ipv6Addresses = Array.FindAll(addresses, a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);

                return ipv6Addresses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class Local
        {
            public static (IPAddress[] ipv4Addresses, IPAddress[] ipv6Addresses) GetLocalIPAddresses()
            {
                try
                {
                    IPAddress[] ipv4Addresses = GetIPv4Addresses();
                    IPAddress[] ipv6Addresses = GetIPv6Addresses();

                    return (ipv4Addresses, ipv6Addresses);
                }
                catch (Exception ex)
                {
                    throw new Exception (ex.Message);
                    // return (new IPAddress[0], new IPAddress[0]); // 发生异常时返回空数组
                }
            }

            public static IPAddress[] GetIPv4Addresses()
            {
                try
                {
                    // 获取所有网络接口
                    NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                    // 使用 LINQ 查询获取所有活动的IPv4地址
                    var ipv4Addresses = interfaces.SelectMany(i => i.GetIPProperties().UnicastAddresses)
                                                  .Where(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                                  .Select(addr => addr.Address)
                                                  .ToArray();

                    return ipv4Addresses;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    // return new IPAddress[0]; // 发生异常时返回空数组
                }
            }

            public static IPAddress[] GetIPv6Addresses()
            {
                try
                {
                    // 获取所有网络接口
                    NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                    // 使用 LINQ 查询获取所有活动的IPv6地址
                    var ipv6Addresses = interfaces.SelectMany(i => i.GetIPProperties().UnicastAddresses)
                                                  .Where(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                                                  .Select(addr => addr.Address)
                                                  .ToArray();

                    return ipv6Addresses;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    // return new IPAddress[0]; // 发生异常时返回空数组
                }
            }
        }
    }
}
