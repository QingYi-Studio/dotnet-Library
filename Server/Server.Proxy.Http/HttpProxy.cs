using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Proxy.Http
{
    public class HttpProxy
    {
        private int port;
        private bool useIPv6;
        private bool outputDetails;

        public HttpProxy(int port = 8080, bool useIPv6 = false, bool outputDetails = false)
        {
            this.port = port;
            this.useIPv6 = useIPv6;
            this.outputDetails = outputDetails;
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
                Thread clientThread = new(() => HandleClient(client));
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            IPAddress localIpAddress = GetLocalIpAddress();

            using (TcpClient localServer = new(localIpAddress.ToString(), 8080)) // 假设中转到本机的 8080 端口
            {
                NetworkStream clientStream = client.GetStream();
                NetworkStream localServerStream = localServer.GetStream();

                try
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = clientStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        localServerStream.Write(buffer, 0, bytesRead);
                        if (outputDetails)
                        {
                            Console.WriteLine($"Sent {bytesRead} bytes to the server: {Encoding.UTF8.GetString(buffer, 0, bytesRead)}");
                        }
                    }

                    // 省略部分代码
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            client.Close();
        }

        private IPAddress GetLocalIpAddress()
        {
            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            if (useIPv6)
            {
                return addresses.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetworkV6) ?? IPAddress.IPv6Loopback;
            }
            else
            {
                return addresses.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork) ?? IPAddress.Loopback;
            }
        }
    }
}
