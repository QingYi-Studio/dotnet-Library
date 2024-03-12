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

### Proxy Http Client

```c#
static void Main()
{
    ProxyHttpClient client = new ProxyHttpClient();
    string response = client.Get("https://www.example.com");
    Console.WriteLine(response);

    // If you need to initiate a POST request, you can use the following code
    // string postData = "key1=value1&key2=value2";
    // string postResponse = client.Post("https://www.example.com/api", postData);
    // Console.WriteLine(postResponse);
}
```

### Http Post File Uploader

```c#
static async Task Main(string[] args)
{
    var uploader = new HttpPostFileUploader();
            
    try
    {
        var url = "https://example.com/upload";
        var filePath = "path/to/file.txt";
                
        var response = await uploader.UploadFile(url, filePath);
        Console.WriteLine($"Upload successful. Response: {response}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occurred: {ex.Message}");
    }
}
```

### Secure Http Client

```c#
static async Task Main(string[] args)
{
    string url = "https://example.com/api/data";
    string certificateFilePath = "path_to_certificate_file.pfx";
    string certificatePassword = "certificate_password";

    var httpClient = new SecureHttpClient(certificateFilePath, certificatePassword);
    var response = await httpClient.GetAsync(url);

    Console.WriteLine(response);
}
```

### Put Request Client

```c#
static async Task Main(string[] args)
{
    string url = "https://example.com/api/resource";
    string requestBody = "{\"key\": \"value\"}";

    var putClient = new PutRequestClient();
    var response = await putClient.SendPutRequestAsync(url, requestBody);

    Console.WriteLine(response);
}
```

### Patch Request Client

```c#
static async Task Main(string[] args)
{
    string url = "https://example.com/api/resource";
    string requestBody = "{\"key\": \"value\"}";
    var customHeaders = new HttpHeaders();
    customHeaders.Add("Authorization", "Bearer your_access_token");

    var patchClient = new PatchRequestClient();
    var response = await patchClient.SendPatchRequestAsync(url, requestBody, customHeaders);

    Console.WriteLine(response);
}
```

### Cookie Request Client

```c#
static async Task Main(string[] args)
{
    string url = "https://example.com/api/resource";

    var cookieClient = new CookieRequestClient();
    var response = await cookieClient.SendRequestWithCookieAsync(url);

    Console.WriteLine(response);
}
```
