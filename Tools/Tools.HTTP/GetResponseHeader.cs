using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Tools.HTTP
{
    public class GetResponseHeader
    {
        public string GetHeader(string url, bool useHttps = false)
        {
            try
            {
                Uri uri = new Uri(url);
                int port = useHttps ? 443 : 80;
                using (TcpClient client = new TcpClient(uri.Host, port))
                {
                    using (NetworkStream ns = client.GetStream())
                    {
                        using (StreamWriter sw = new StreamWriter(ns))
                        {
                            string protocol = useHttps ? "https" : "http";
                            string request = $"HEAD {uri.PathAndQuery} {protocol.ToUpper()}/1.1\r\nHost: {uri.Host}\r\nConnection: close\r\n\r\n";
                            byte[] bytesToSend = Encoding.ASCII.GetBytes(request);
                            sw.Write(request);
                            sw.Flush();

                            using (StreamReader sr = new StreamReader(ns))
                            {
                                string response = "";
                                string line;
                                while ((line = sr.ReadLine() ?? "") != null)
                                {
                                    if (string.IsNullOrWhiteSpace(line))
                                    {
                                        break;
                                    }
                                    response += line + "\n";
                                }
                                return response;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
