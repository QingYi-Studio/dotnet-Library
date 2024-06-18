using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

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
            // 基础的HTTP GET请求
            string request = $"GET / HTTP/1.1\r\nHost: {url}\r\n\r\n";

            // 使用 Socket 发送 HTTP 请求并接收响应
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                // 连接到主机的80端口（HTTP）
                socket.Connect(url, 80);

                byte[] requestBytes = Encoding.ASCII.GetBytes(request);
                socket.Send(requestBytes);

                // 接收响应并转换成字符串
                byte[] buffer = new byte[1024];
                int received = socket.Receive(buffer);
                string response = Encoding.ASCII.GetString(buffer, 0, received);

                // 从响应中提取 IPv4 地址
                string ipAddressString = ExtractIPv4Address(response);
                if (ipAddressString != null)
                {
                    if (IPAddress.TryParse(ipAddressString, out IPAddress ipAddress))
                    {
                        return ipAddress;
                    }
                }

                throw new Exception($"Cannot get {url} IPV4 address");
            }
        }

        // 从HTTP响应中提取IPv4地址
        private static string ExtractIPv4Address(string response)
        {
            string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
            Match match = Regex.Match(response, pattern);

            if (match.Success)
            {
                return match.Value;
            }

            return null;
        }

        /// <summary>
        /// Get IPv6 Address<br></br>
        /// 获取IPv6地址
        /// </summary>
        /// <param name="url">Url|链接</param>
        /// <returns>IP</returns>
        /// <exception cref="Exception"></exception>
        public static IPAddress GetIPv6Address(string url)
        {
            // 基础的HTTP GET请求
            string request = $"GET / HTTP/1.1\r\nHost: {url}\r\n\r\n";

            // 使用 Socket 发送 HTTP 请求并接收响应
            using (Socket socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp))
            {
                // 连接到主机的80端口（HTTP），指定IPv6地址
                socket.Connect($"{url}", 80);

                byte[] requestBytes = Encoding.ASCII.GetBytes(request);
                socket.Send(requestBytes);

                // 接收响应并转换成字符串
                byte[] buffer = new byte[1024];
                int received = socket.Receive(buffer);
                string response = Encoding.ASCII.GetString(buffer, 0, received);

                // 从响应中提取 IPv6 地址
                string ipAddressString = ExtractIPv6Address(response);
                if (ipAddressString != null)
                {
                    if (IPAddress.TryParse(ipAddressString, out IPAddress ipAddress))
                    {
                        return ipAddress;
                    }
                }

                throw new Exception($"Cannot get {url} IPV6 address");
            }
        }

        // 从HTTP响应中提取IPv6地址
        private static string ExtractIPv6Address(string response)
        {
            string pattern = @"([0-9A-Fa-f]{1,4}(:[0-9A-Fa-f]{1,4}){7})"; // 匹配IPv6地址格式
            Match match = Regex.Match(response, pattern);

            if (match.Success)
            {
                return match.Value;
            }

            return null;
        }
    }
}
