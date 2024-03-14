using System.Net;

namespace Tools.LocalServer
{
    public class LocalServer
    {
        private string? _rootFolder;
        private bool _showConsole;

        public string RootFolder => _rootFolder!;

        public void Initialize(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        public void Start(bool showConsole, bool useHttps = false, int port = 8080)
        {
            _showConsole = showConsole;

            Thread serverThread = new(() =>
            {
                var protocol = useHttps ? "https" : "http";
                var listener = new HttpListener();
                listener.Prefixes.Add($"{protocol}://localhost:{port}/");

                try
                {
                    listener.Start();
                    LogToConsole($"Server started at {protocol}://localhost:{port}/");

                    while (true)
                    {
                        var context = listener.GetContext();
                        var requestUrl = context.Request.Url!.AbsolutePath;
                        var filePath = Path.Combine(_rootFolder!, requestUrl.TrimStart('/'));

                        if (File.Exists(filePath))
                        {
                            using var fileStream = File.OpenRead(filePath);
                            context.Response.ContentType = "application/octet-stream";
                            context.Response.ContentLength64 = new FileInfo(filePath).Length;
                            fileStream.CopyTo(context.Response.OutputStream);
                        }
                        else
                        {
                            context.Response.StatusCode = 404;
                        }

                        context.Response.Close();
                    }
                }
                catch (Exception ex)
                {
                    LogToConsole($"Error: {ex.Message}");
                }
                finally
                {
                    listener.Stop();
                }
            });

            serverThread.Start();
        }

        private void LogToConsole(string message)
        {
            if (_showConsole)
            {
                Console.WriteLine(message);
            }
        }

        public void Dispose()
        {
            // 停止服务器
            var listener = new HttpListener();
            listener.Stop();
            LogToConsole("Server stopped");
        }
    }

    class U
    {
        private LocalServer? _localServer;

        public void StartServer()
        {
            _localServer = new LocalServer();
            _localServer.Initialize(@".\abdc");
            _localServer.Start(showConsole: true, useHttps: false, port: 8080);

            Console.WriteLine("Local server started. Press any key to stop...");
            Console.ReadKey();
        }

        public void StopServer()
        {
            // 停止服务器
            _localServer?.Dispose();
        }
    }
}
