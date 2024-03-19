# Server.GetLocalHost

Alias：Server.GetLocalInfo

A C# Library that can get some local info about the server.

Support .NET 6.0 to .NET 8.0 (Windows only).

## Usage

### Get Local IP

```c#
LocalIP = IP.GetIP();
Console.Writeline(LocalIP);
```

### Get Local Port

```c#
Console.WriteLine("Active TCP Ports:");
foreach (int port in PortHelper.GetActiveTcpPorts())
{
    Console.WriteLine(port);
}

Console.WriteLine("\nActive UDP Ports:");
foreach (int port in PortHelper.GetActiveUdpPorts())
{
    Console.WriteLine(port);
}
```

### Get Connect Info

```c#
// Get the current connection information.
ConnectionInfo currentConnection = ConnectionInfo.GetCurrentConnectionInfo();

// Print the connection information using Console.WriteLine.
Console.WriteLine("Using Console.WriteLine:");
Console.WriteLine($"Host: {currentConnection.Host}, Port: {currentConnection.Port}, Network Type: {currentConnection.NetworkType}");

// Print the connection information in a table using the internal print function.
Console.WriteLine("\nUsing internal print function:");
currentConnection.PrintConnectionInfo();
```
