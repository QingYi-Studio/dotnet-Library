# Picture.AXmlToSvg

A simple Nuget package can convert Android Xml image files to SVG.

If your XML file is open as garbled and cannot be converted, you can try using [this](https://github.com/Grey-Wind/AXMLPrinter3-GUI) to try to solve it.

## Usage

```c#
string input = "C:\inputPath\input.xml";

string output = "C:\outputPath\output.svg";

// Sync
StartConvert.Start(input, output);

// Async
await StartConvert.StartAsync(input, output);
```
