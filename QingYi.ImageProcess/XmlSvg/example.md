# Svg To Xml

```
using QingYi.ImageProcess.XmlSvg;

SvgToXml svg = new()
{
    InputFilePath = "output.svg",
    OutputFilePath = "reo.xml",
    IncludeXmlDeclaration = false,
};
Console.WriteLine(svg.Convert());
```

# Xml To Svg

```
using QingYi.ImageProcess.XmlSvg;

XmlToSvg svg = new()
{
    InputFilePath = "output.xml",
    OutputFilePath = "reo.svg",
};
Console.WriteLine(svg.Convert());
```
