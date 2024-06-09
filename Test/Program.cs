using QingYi.ImageProcess.XmlSvg;

XmlToSvg xmlToSvg = new()
{
    InputFilePath = "ic_launcher_foreground.xml",
    OutputFilePath = "output.svg"
};
xmlToSvg.Convert();
