# Png To Ico

```
PngToIco pngToIco = new PngToIco
{
    Quality = long.Parse("100")
};
pngToIco.Convert(pngFilePath, icoFilePath);
```

# Ico To Png

```
IcoToPng icoToPng = new IcoToPng
{
    Quality = long.Parse("90")
};
icoToPng.Convert(icoFilePath, pngFilePath);
```
