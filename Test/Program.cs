using QingYi.ImageProcess.XmlSvg;

SvgToXml svg = new()
{
    InputFilePath = "output.svg",
    OutputFilePath = "reo.xml"
};
Console.WriteLine(svg.Convert());