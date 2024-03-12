# Tools.HTTP

A library that contains most of the basic HTTP/HTTPS communication.

Attention:
- 1.0.0 and later are for .NET 8.0

## Usage:

### Json Post Request

```c#
public static async Task JsonData()
{
    try
    {
        JsonPostRequest jsonRequest = new JsonPostRequest();

        string url = "https://example.com/api/endpoint";
        string jsonData = "{\"key\": \"value\"}";

        string response = await jsonRequest.PostJsonData(url, jsonData);

        Console.WriteLine(response);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred: " + ex.Message);
    }
}

public static async Task LocalJson()
{
    try
    {
        JsonPostRequest jsonRequest = new JsonPostRequest();

        string url = "https://example.com/api/endpoint";
        string filePath = "path/to/your/json/file.json";

        string response = await jsonRequest.PostJsonDataFromFile(url, filePath);

        Console.WriteLine(response);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred: " + ex.Message);
    }
}
```

### Http Get Request

```c#
static async Task Main(string[] args)
{
    HttpGetRequest httpRequest = new HttpGetRequest();

    string url = "https://api.example.com/data";

    string response = await httpRequest.GetResponseAsync(url);

    Console.WriteLine("Response from " + url + ":");
    Console.WriteLine(response);
}
```

### Get Response Header

```c#
static void Main(string[] args)
{
    string url = "https://www.example.com";
    bool useHttps = true; // or false, default false
    GetResponseHeader headerGetter = new GetResponseHeader();
    string header = headerGetter.GetHeader(url, useHttps);
    Console.WriteLine(header);
}
```

### Http Get Request With Params

```c#
static async Task Main()
{
    var getRequestHelper = new HttpGetRequestWithParams();
    var queryParams = new Dictionary<string, string>
    {
        { "key1", "value1" },
        { "key2", "value2" }
    };

    string url = "https://api.example.com/data";
    string response = await getRequestHelper.GetAsync(url, queryParams);

    Console.WriteLine(response);
}
```
