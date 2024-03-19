using System.Net;

namespace Server.GetLocalHost
{
    public class ConnectionInfo
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? NetworkType { get; set; }

        public string ToTableString(int hostWidth, int portWidth, int networkWidth)
        {
            return string.Format("| {0,-" + hostWidth + "} | {1,-" + portWidth + "} | {2,-" + networkWidth + "} |", Host, Port, NetworkType);
        }

        public void PrintConnectionInfo()
        {
            // 计算表格列宽
            int hostWidth = Math.Max(Host!.Length, 4); // 最小宽度为4
            int portWidth = Math.Max(Port.ToString().Length, 4); // 最小宽度为4
            int networkWidth = Math.Max(NetworkType!.Length, 7); // 最小宽度为7

            // 打印连接信息表格
            Console.WriteLine("| Host           | Port | Network   |");
            Console.WriteLine("------------------------------------");
            Console.WriteLine(ToTableString(hostWidth, portWidth, networkWidth));
        }

        public static ConnectionInfo GetCurrentConnectionInfo()
        {
            // 获取本地计算机的主机名
            string hostName = Dns.GetHostName();

            // 获取主机的IP地址列表
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

            // 假设连接的目标是第一个找到的IP地址
            IPAddress targetIpAddress = ipAddresses[0];

            // 创建一个表示连接的IP终结点
            IPEndPoint endPoint = new(targetIpAddress, 0);

            // 根据终结点判断网络类型
            string networkType = endPoint.AddressFamily.ToString();

            // 构建连接信息对象
            ConnectionInfo connectionInfo = new()
            {
                Host = hostName,
                Port = endPoint.Port,
                NetworkType = networkType
            };

            return connectionInfo;
        }
    }
}
