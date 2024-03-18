# Color.Conversion

A C# library that can quickly convert multiple color types.

Attention:
- 1.0.0 and later is for .NET 8.0

## Usage

### RGB to CMYK

```c#
int red = 255;
int green = 0;
int blue = 0;
double cyan, magenta, yellow, black;

RgbToCmyk.Convert(red, green, blue, out cyan, out magenta, out yellow, out black);

Console.WriteLine("CMYK: " + cyan.ToString("0.00") + ", " + magenta.ToString("0.00") + ", " + yellow.ToString("0.00") + ", " + black.ToString("0.00"));
```

### RGB to HSV

```c#
int red = 255;
int green = 0;
int blue = 0;
double hue, saturation, value;

RgbToHsv.Convert(red, green, blue, out hue, out saturation, out value);

Console.WriteLine("HSV: " + hue.ToString("0.00") + ", " + saturation.ToString("0.00") + ", " + value.ToString("0.00"));
```

### RGB to Hex

```c#
int red = 255;
int green = 0;
int blue = 128;

string hexColor = RgbToHexConverter.Convert(red, green, blue);

Console.WriteLine("Hex Color: " + hexColor);
```

### RGB to HSL

```c#
int red = 255;
int green = 165;
int blue = 0;

var hslColor = RgbToHsl.Convert(red, green, blue);

Console.WriteLine($"Hue: {hslColor.Item1}°, Saturation: {hslColor.Item2}, Lightness: {hslColor.Item3}");
```

### CMYK to RGB

```c#
double cyan = 0.5;
double magenta = 0.2;
double yellow = 0.1;
double black = 0.3;
int red, green, blue;

CmykToRgb.Convert(cyan, magenta, yellow, black, out red, out green, out blue);

Console.WriteLine("RGB: " + red + ", " + green + ", " + blue);
```

### CMYK to HSV

```c#
double cyan = 0;
double magenta = 0.5;
double yellow = 1;
double black = 0;
double hue, saturation, value;

CmykToHsv.Convert(cyan, magenta, yellow, black, out hue, out saturation, out value);

Console.WriteLine("HSV: " + hue.ToString("0.00") + ", " + saturation.ToString("0.00") + ", " + value.ToString("0.00"));
```

### CMYK to Hex

```c#
double cyan = 0;
double magenta = 0.5;
double yellow = 1;
double black = 0;

string hexColor = CmykToHex.Convert(cyan, magenta, yellow, black);

Console.WriteLine("Hex Color: " + hexColor);
```

### CMYK to HSL

```c#
int cyan = 50;
int magenta = 25;
int yellow = 0;
int key = 10;

var hslColor = CmykToHslConverter.Convert(cyan, magenta, yellow, key);

Console.WriteLine($"Hue: {hslColor.Item1}°, Saturation: {hslColor.Item2}, Lightness: {hslColor.Item3}");
```

### HSV to RGB

```c#
double hue = 0;
double saturation = 1;
double value = 1;
int red, green, blue;

HsvToRgb.Convert(hue, saturation, value, out red, out green, out blue);

Console.WriteLine("RGB: " + red + ", " + green + ", " + blue);
```

### HSV to CMYK

```c#
double hue = 120;
double saturation = 0.5;
double value = 0.75;
double cyan, magenta, yellow, black;

HsvToCmyk.Convert(hue, saturation, value, out cyan, out magenta, out yellow, out black);

Console.WriteLine("CMYK: " + cyan.ToString("0.00") + ", " + magenta.ToString("0.00") + ", " + yellow.ToString("0.00") + ", " + black.ToString("0.00"));
```

### HSV to Hex

```c#
double hue = 120;
double saturation = 0.5;
double value = 0.75;

string hexColor = HsvToHex.Convert(hue, saturation, value);

Console.WriteLine("Hex Color: " + hexColor);
```

### HSV to HSL

```c#
double hue = 200;
double saturation = 0.75;
double value = 0.8;

var hslColor = HsvToHsl.Convert(hue, saturation, value);

Console.WriteLine($"Hue: {hslColor.Item1}°, Saturation: {hslColor.Item2}, Lightness: {hslColor.Item3}");
```

### Hex to RGB

```c#
string hexColor = "#FFA500";
(int red, int green, int blue) = HexToRgb.Convert_(hexColor);

Console.WriteLine("Red: " + red);
Console.WriteLine("Green: " + green);
Console.WriteLine("Blue: " + blue);
```

### Hex to CMYK

```c#
string hexColor = "#FFA500";
(double cyan, double magenta, double yellow, double black) = HexToCmyk.Convert_(hexColor);

Console.WriteLine("Cyan: " + cyan);
Console.WriteLine("Magenta: " + magenta);
Console.WriteLine("Yellow: " + yellow);
Console.WriteLine("Black: " + black);
```

### Hex to HSV

```c#
string hexColor = "#FFA500";

var hsvColor = HexToHsv.Convert_(hexColor);

Console.WriteLine($"Hue: {hsvColor.Item1}°, Saturation: {hsvColor.Item2}, Value: {hsvColor.Item3}");
```

### Hex to HSL

```c#
string hexColor = "#008080";

var hslColor = HexToHsl.Convert(hexColor);

Console.WriteLine($"Hue: {hslColor.Item1}°, Saturation: {hslColor.Item2}, Lightness: {hslColor.Item3}");
```

### HSL to RGB

```c#
int h = 200;
double s = 0.75;
double l = 0.5;

var rgbColor = HslToRgb.Convert(h, s, l);

Console.WriteLine($"Red: {rgbColor.Item1}, Green: {rgbColor.Item2}, Blue: {rgbColor.Item3}");
```

### HSL to CMYK

```c#
int h = 200;
double s = 0.75;
double l = 0.5;

var cmykColor = HslToCmyk.Convert(h, s, l);

Console.WriteLine($"Cyan: {cmykColor.Item1}, Magenta: {cmykColor.Item2}, Yellow: {cmykColor.Item3}, Black: {cmykColor.Item4}");
```

### HSL to HSV

```c#
int h = 200;
double s = 0.75;
double l = 0.5;

var hsvColor = HslToHsv.Convert(h, s, l);

Console.WriteLine($"Hue: {hsvColor.Item1}, Saturation: {hsvColor.Item2}, Value: {hsvColor.Item3}");
```

### HSL to Hex

```c#
int h = 200;
double s = 0.75;
double l = 0.5;

string hexColor = HslToHex.Convert(h, s, l);

Console.WriteLine($"Hex Color: {hexColor}");
```
