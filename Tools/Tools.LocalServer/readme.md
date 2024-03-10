# Local Server

A C# library that can open a local server easily.

You can use it to test you website, such as static html file.

Attention:
- 1.0.0-1.1.0 is for .NET 6.0
- 1.2.0 is for .NET 7.0
- 1.3.0 and later is for .NET 8.0

Usage:

```c#
private LocalServer _localServer;

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
```
