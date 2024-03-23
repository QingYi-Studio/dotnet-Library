using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.Proxy.Http
{
    public class HttpProxy
    {
        private int port;
        private string username;
        private string password;
        private bool useIPv6;

        public HttpProxy(int port, string? username = null, string? password = null, bool useIPv6 = false)
        {
            this.port = port;
            this.username = username!;
            this.password = password!;
            this.useIPv6 = useIPv6;
        }

        public void Start()
        {
            IPAddress localIpAddress = GetLocalIpAddress();

            if (localIpAddress == null)
            {
                Console.WriteLine("Failed to get local IP address. Proxy server cannot start.");
                return;
            }

            TcpListener listener = new(localIpAddress, port);
            listener.Start();
            Console.WriteLine($"HTTP Proxy server started on {localIpAddress}:{port}");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream clientStream = client.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = clientStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // 在这里可以处理客户端发送过来的数据，并根据HTTP/HTTPS协议进行转发
                targetServerStream.Write(buffer, 0, bytesRead);
            }

            client.Close();

        }

        private IPAddress GetLocalIpAddress()
        {
            if (useIPv6)
            {
                return Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetworkV6)!;
            }
            else
            {
                return Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)!;
            }
        }
    }
}
