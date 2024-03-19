using System.Net.NetworkInformation;
using System.Net;

namespace Server.GetLocalHost
{
    public class Port
    {
        public static List<int> GetActiveTcpPorts()
        {
            List<int> activePorts = new();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = properties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in endPoints)
            {
                activePorts.Add(endPoint.Port);
            }

            return activePorts;
        }

        public static List<int> GetActiveUdpPorts()
        {
            List<int> activePorts = new();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = properties.GetActiveUdpListeners();

            foreach (IPEndPoint endPoint in endPoints)
            {
                activePorts.Add(endPoint.Port);
            }

            return activePorts;
        }
    }
}
