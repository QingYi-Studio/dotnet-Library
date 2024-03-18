using System;
using System.Drawing;

namespace Color.Conversion
{
    // RGB->
    public class RgbToCmyk
    {
        public static void Convert(int red, int green, int blue, out double cyan, out double magenta, out double yellow, out double black)
        {
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            double k = 1 - Math.Max(Math.Max(r, g), b);
            double c = (1 - r - k) / (1 - k);
            double m = (1 - g - k) / (1 - k);
            double y = (1 - b - k) / (1 - k);

            cyan = c;
            magenta = m;
            yellow = y;
            black = k;
        }
    }

    public class RgbToHsv
    {
        public static void Convert(int red, int green, int blue, out double hue, out double saturation, out double value)
        {
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            double max = Math.Max(Math.Max(r, g), b);
            double min = Math.Min(Math.Min(r, g), b);
            double delta = max - min;

            // 计算Hue
            double h = 0;
            if (delta != 0)
            {
                if (max == r)
                    h = (g - b) / delta % 6;
                else if (max == g)
                    h = (b - r) / delta + 2;
                else if (max == b)
                    h = (r - g) / delta + 4;
            }
            hue = h * 60;

            // 计算Saturation
            double s = 0;
            if (max != 0)
                s = delta / max;
            saturation = s;

            // 计算Value
            double v = max;
            value = v;
        }
    }

    public class RgbToHex
    {
        public static string Convert(int red, int green, int blue)
        {
            string hex = "#" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
            return hex;
        }
    }

    public class RgbToHsl
    {
        public static (int, double, double) Convert(int red, int green, int blue)
        {
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            double cmax = Math.Max(Math.Max(r, g), b);
            double cmin = Math.Min(Math.Min(r, g), b);
            double delta = cmax - cmin;

            double h = 0;
            if (delta != 0)
            {
                if (cmax == r)
                    h = 60 * (((g - b) / delta) % 6);
                else if (cmax == g)
                    h = 60 * (((b - r) / delta) + 2);
                else
                    h = 60 * (((r - g) / delta) + 4);
            }

            double l = (cmax + cmin) / 2;

            double s = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * l - 1));

            return ((int)Math.Round(h), Math.Round(s, 2), Math.Round(l, 2));
        }
    }

    // CMYK->
    public class CmykToRgb
    {
        public static void Convert(double cyan, double magenta, double yellow, double black, out int red, out int green, out int blue)
        {
            double c = cyan;
            double m = magenta;
            double y = yellow;
            double k = black;

            double r = (1 - c) * (1 - k);
            double g = (1 - m) * (1 - k);
            double b = (1 - y) * (1 - k);

            red = (int)(r * 255);
            green = (int)(g * 255);
            blue = (int)(b * 255);
        }
    }

    public class CmykToHsv
    {
        public static void Convert(double cyan, double magenta, double yellow, double black, out double hue, out double saturation, out double value)
        {
            double c = cyan;
            double m = magenta;
            double y = yellow;
            double k = black;

            double r = 1 - Math.Min(1, c * (1 - k) + k);
            double g = 1 - Math.Min(1, m * (1 - k) + k);
            double b = 1 - Math.Min(1, y * (1 - k) + k);

            double max = Math.Max(Math.Max(r, g), b);
            double min = Math.Min(Math.Min(r, g), b);
            double delta = max - min;

            // 计算Hue
            double h = 0;
            if (delta != 0)
            {
                if (max == r)
                    h = (g - b) / delta % 6;
                else if (max == g)
                    h = (b - r) / delta + 2;
                else if (max == b)
                    h = (r - g) / delta + 4;
            }
            hue = h * 60;

            // 计算Saturation
            double s = 0;
            if (max != 0)
                s = delta / max;
            saturation = s;

            // 计算Value
            double v = max;
            value = v;
        }
    }

    public class CmykToHex
    {
        public static string Convert(double cyan, double magenta, double yellow, double black)
        {
            double c = cyan;
            double m = magenta;
            double y = yellow;
            double k = black;

            int red = (int)((1 - Math.Min(1, c * (1 - k) + k)) * 255);
            int green = (int)((1 - Math.Min(1, m * (1 - k) + k)) * 255);
            int blue = (int)((1 - Math.Min(1, y * (1 - k) + k)) * 255);

            string hexColor = "#" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
            return hexColor;
        }
    }

    public class CmykToHsl
    {
        public static (int, double, double) Convert(int cyan, int magenta, int yellow, int key)
        {
            double c = cyan / 100.0;
            double m = magenta / 100.0;
            double y = yellow / 100.0;
            double k = key / 100.0;

            double r = (1 - Math.Min(1, c * (1 - k) + k)) * 255;
            double g = (1 - Math.Min(1, m * (1 - k) + k)) * 255;
            double b = (1 - Math.Min(1, y * (1 - k) + k)) * 255;

            double h, s, l;

            double cmax = Math.Max(Math.Max(r, g), b);
            double cmin = Math.Min(Math.Min(r, g), b);
            double delta = cmax - cmin;

            l = (cmax + cmin) / 2;

            if (delta == 0)
            {
                h = 0;
                s = 0;
            }
            else
            {
                if (cmax == r)
                    h = 60 * (((g - b) / delta) % 6);
                else if (cmax == g)
                    h = 60 * (((b - r) / delta) + 2);
                else
                    h = 60 * (((r - g) / delta) + 4);

                s = delta / (1 - Math.Abs(2 * l - 1));
            }

            return ((int)Math.Round(h), Math.Round(s, 2), Math.Round(l, 2));
        }
    }

    // HSV->
    public class HsvToRgb
    {
        public static void Convert(double hue, double saturation, double value, out int red, out int green, out int blue)
        {
            double h = hue;
            double s = saturation;
            double v = value;

            int hi = (int)Math.Floor(h / 60) % 6;
            double f = h / 60 - Math.Floor(h / 60);
            double p = v * (1 - s);
            double q = v * (1 - f * s);
            double t = v * (1 - (1 - f) * s);

            switch (hi)
            {
                case 0:
                    red = (int)(v * 255);
                    green = (int)(t * 255);
                    blue = (int)(p * 255);
                    break;
                case 1:
                    red = (int)(q * 255);
                    green = (int)(v * 255);
                    blue = (int)(p * 255);
                    break;
                case 2:
                    red = (int)(p * 255);
                    green = (int)(v * 255);
                    blue = (int)(t * 255);
                    break;
                case 3:
                    red = (int)(p * 255);
                    green = (int)(q * 255);
                    blue = (int)(v * 255);
                    break;
                case 4:
                    red = (int)(t * 255);
                    green = (int)(p * 255);
                    blue = (int)(v * 255);
                    break;
                default:
                    red = (int)(v * 255);
                    green = (int)(p * 255);
                    blue = (int)(q * 255);
                    break;
            }
        }
    }

    public class HsvToCmyk
    {
        public static void Convert(double hue, double saturation, double value, out double cyan, out double magenta, out double yellow, out double black)
        {
            double h = hue;
            double s = saturation;
            double v = value;

            double c = v * s;
            double m = (v - c) * s;
            double y = 0;
            double k = 1 - v;

            double hi = Math.Floor(h / 60) % 6;
            double f = h / 60 - Math.Floor(h / 60);
            double p = v * (1 - s);
            double q = v * (1 - f * s);
            double t = v * (1 - (1 - f) * s);

            switch ((int)hi)
            {
                case 0:
                    y = t;
                    m = p;
                    break;
                case 1:
                    y = v;
                    m = p;
                    break;
                case 2:
                    c = p;
                    y = t;
                    break;
                case 3:
                    c = p;
                    y = v;
                    break;
                case 4:
                    c = t;
                    y = p;
                    break;
                default:
                    y = p;
                    m = t;
                    break;
            }

            cyan = c;
            magenta = m;
            yellow = y;
            black = k;
        }
    }

    public class HsvToHex
    {
        public static string Convert(double hue, double saturation, double value)
        {
            double c = value * saturation;
            double x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
            double m = value - c;

            double r, g, b;

            if (hue >= 0 && hue < 60)
            {
                r = c;
                g = x;
                b = 0;
            }
            else if (hue >= 60 && hue < 120)
            {
                r = x;
                g = c;
                b = 0;
            }
            else if (hue >= 120 && hue < 180)
            {
                r = 0;
                g = c;
                b = x;
            }
            else if (hue >= 180 && hue < 240)
            {
                r = 0;
                g = x;
                b = c;
            }
            else if (hue >= 240 && hue < 300)
            {
                r = x;
                g = 0;
                b = c;
            }
            else
            {
                r = c;
                g = 0;
                b = x;
            }

            int red = (int)((r + m) * 255);
            int green = (int)((g + m) * 255);
            int blue = (int)((b + m) * 255);

            string hexColor = "#" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
            return hexColor;
        }
    }

    public class HsvToHsl
    {
        public static (int, double, double) Convert(double hue, double saturation, double value)
        {
            double h = hue;
            double s = saturation;
            double v = value;

            double l = (2 - s) * v / 2;

            if (l != 0)
            {
                if (l == 1)
                {
                    s = 0;
                }
                else if (l < 0.5)
                {
                    s = s * v / (l * 2);
                }
                else
                {
                    s = s * v / (2 - l * 2);
                }
            }

            return ((int)Math.Round(h), Math.Round(s, 2), Math.Round(l, 2));
        }
    }

    // Hex->
    public class HexToRgb
    {
        public static (int, int, int) Convert_(string hex)
        {
            hex = hex.TrimStart('#');

            int red = Convert.ToInt32(hex[..2], 16);
            int green = Convert.ToInt32(hex.Substring(2, 2), 16);
            int blue = Convert.ToInt32(hex.Substring(4, 2), 16);

            return (red, green, blue);
        }
    }

    public class HexToCmyk
    {
        public static (double, double, double, double) Convert_(string hex)
        {
            hex = hex.TrimStart('#');

            int red = Convert.ToInt32(hex[..2], 16);
            int green = Convert.ToInt32(hex.Substring(2, 2), 16);
            int blue = Convert.ToInt32(hex.Substring(4, 2), 16);

            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            double k = 1 - Math.Max(Math.Max(r, g), b);
            double c = (1 - r - k) / (1 - k);
            double m = (1 - g - k) / (1 - k);
            double y = (1 - b - k) / (1 - k);

            return (c, m, y, k);
        }
    }

    public class HexToHsv
    {
        public static (int, double, double) Convert_(string hex)
        {
            hex = hex.TrimStart('#');

            int red = Convert.ToInt32(hex[..2], 16);
            int green = Convert.ToInt32(hex.Substring(2, 2), 16);
            int blue = Convert.ToInt32(hex.Substring(4, 2), 16);

            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            double cmax = Math.Max(Math.Max(r, g), b);
            double cmin = Math.Min(Math.Min(r, g), b);
            double delta = cmax - cmin;

            double h = 0;
            if (delta != 0)
            {
                if (cmax == r)
                    h = 60 * (((g - b) / delta) % 6);
                else if (cmax == g)
                    h = 60 * (((b - r) / delta) + 2);
                else
                    h = 60 * (((r - g) / delta) + 4);
            }

            double s = cmax == 0 ? 0 : delta / cmax;
            double v = cmax;

            return ((int)Math.Round(h), Math.Round(s, 2), Math.Round(v, 2));
        }
    }

    public class HexToHsl
    {
        public static (int, double, double) Convert(string hexColor)
        {
            int r = int.Parse(hexColor.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(hexColor.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(hexColor.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

            double red = r / 255.0;
            double green = g / 255.0;
            double blue = b / 255.0;

            double cmax = Math.Max(Math.Max(red, green), blue);
            double cmin = Math.Min(Math.Min(red, green), blue);
            double delta = cmax - cmin;

            double h, s, l;

            l = (cmax + cmin) / 2;

            if (delta == 0)
            {
                h = 0;
                s = 0;
            }
            else
            {
                if (cmax == red)
                    h = 60 * (((green - blue) / delta) % 6);
                else if (cmax == green)
                    h = 60 * (((blue - red) / delta) + 2);
                else
                    h = 60 * (((red - green) / delta) + 4);

                s = delta / (1 - Math.Abs(2 * l - 1));
            }

            return ((int)Math.Round(h), Math.Round(s, 2), Math.Round(l, 2));
        }
    }

    // HSL->
    public class HslToRgb
    {
        public static (int, int, int) Convert(int h, double s, double l)
        {
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs((h / 60.0 % 2) - 1));
            double m = l - c / 2;

            double r1, g1, b1;

            if (h >= 0 && h < 60)
            {
                r1 = c;
                g1 = x;
                b1 = 0;
            }
            else if (h >= 60 && h < 120)
            {
                r1 = x;
                g1 = c;
                b1 = 0;
            }
            else if (h >= 120 && h < 180)
            {
                r1 = 0;
                g1 = c;
                b1 = x;
            }
            else if (h >= 180 && h < 240)
            {
                r1 = 0;
                g1 = x;
                b1 = c;
            }
            else if (h >= 240 && h < 300)
            {
                r1 = x;
                g1 = 0;
                b1 = c;
            }
            else
            {
                r1 = c;
                g1 = 0;
                b1 = x;
            }

            int r = (int)((r1 + m) * 255);
            int g = (int)((g1 + m) * 255);
            int b = (int)((b1 + m) * 255);

            return (r, g, b);
        }
    }

    public class HslToCmyk
    {
        public static (double, double, double, double) Convert(int h, double s, double l)
        {
            (int, int, int) rgbColor = HslToRgb(h, s, l);
            return RgbToCmyk(rgbColor.Item1, rgbColor.Item2, rgbColor.Item3);
        }

        private static (int, int, int) HslToRgb(int h, double s, double l)
        {
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs((h / 60.0 % 2) - 1));
            double m = l - c / 2;

            double r1, g1, b1;

            if (h >= 0 && h < 60)
            {
                r1 = c;
                g1 = x;
                b1 = 0;
            }
            else if (h >= 60 && h < 120)
            {
                r1 = x;
                g1 = c;
                b1 = 0;
            }
            else if (h >= 120 && h < 180)
            {
                r1 = 0;
                g1 = c;
                b1 = x;
            }
            else if (h >= 180 && h < 240)
            {
                r1 = 0;
                g1 = x;
                b1 = c;
            }
            else if (h >= 240 && h < 300)
            {
                r1 = x;
                g1 = 0;
                b1 = c;
            }
            else
            {
                r1 = c;
                g1 = 0;
                b1 = x;
            }

            int r = (int)((r1 + m) * 255);
            int g = (int)((g1 + m) * 255);
            int b = (int)((b1 + m) * 255);

            return (r, g, b);
        }

        private static (double, double, double, double) RgbToCmyk(int r, int g, int b)
        {
            double c = 1 - (r / 255.0);
            double m = 1 - (g / 255.0);
            double y = 1 - (b / 255.0);
            double k = Math.Min(c, Math.Min(m, y));

            if (k == 1)
            {
                c = 0;
                m = 0;
                y = 0;
            }
            else
            {
                c = (c - k) / (1 - k);
                m = (m - k) / (1 - k);
                y = (y - k) / (1 - k);
            }

            return (c, m, y, k);
        }
    }

    public class HslToHsv
    {
        public static (int, double, double) Convert(int h, double s, double l)
        {
            double v = l + s * Math.Min(l, 1 - l);
            double sv = v == 0 ? 0 : 2 * (1 - l / v);

            return (h, sv, v);
        }
    }

    public class HslToHex
    {
        public static string Convert(int h, double s, double l)
        {
            int r, g, b;
            HslToRgb(h, s, l, out r, out g, out b);

            string hex = $"#{r:X2}{g:X2}{b:X2}";
            return hex;
        }

        private static void HslToRgb(int h, double s, double l, out int r, out int g, out int b)
        {
            if (s == 0)
            {
                r = g = b = (int)(l * 255);
            }
            else
            {
                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;
                double hk = h / 360.0;

                double[] rgb = new double[3];
                rgb[0] = hk + 1.0 / 3.0; // Red
                rgb[1] = hk; // Green
                rgb[2] = hk - 1.0 / 3.0; // Blue

                for (int i = 0; i < 3; i++)
                {
                    if (rgb[i] < 0)
                        rgb[i] += 1;
                    else if (rgb[i] > 1)
                        rgb[i] -= 1;

                    if (rgb[i] < 1.0 / 6.0)
                        rgb[i] = p + ((q - p) * 6 * rgb[i]);
                    else if (rgb[i] < 0.5)
                        rgb[i] = q;
                    else if (rgb[i] < 2.0 / 3.0)
                        rgb[i] = p + ((q - p) * 6 * (2.0 / 3.0 - rgb[i]));
                    else
                        rgb[i] = p;
                }

                r = (int)(rgb[0] * 255);
                g = (int)(rgb[1] * 255);
                b = (int)(rgb[2] * 255);
            }
        }
    }
}
