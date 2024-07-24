# Server.Proxy.Http

A simple package to open a http server.(NOT HTTPS!!!)

## Usage

```c#
// Instantiate the HttpProxy class with default port 8080 and no detailed output
HttpProxy httpProxy = new HttpProxy();

// If you need to customize the port and enable detailed output, you can instantiate as follows
// HttpProxy httpProxy = new HttpProxy(8888, false);

// Start the proxy server
httpProxy.Start();

Console.ReadLine(); // Prevents the console application from exiting immediately
```
