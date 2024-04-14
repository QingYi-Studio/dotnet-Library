using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

namespace Image.AndroidXml2Svg
{
    public class Convert
    {
        public static void ConvertXmlToSvg(string xmlFilePath)
        {
            try
            {
                // 读取XML文件
                XmlDocument xmlDoc = new();
                xmlDoc.Load(xmlFilePath);

                // 替换标记和属性
                string svgContent = xmlDoc.InnerXml;
                svgContent = Regex.Replace(svgContent, "<vector", "<svg", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "xmlns:android=\"http://schemas.android.com/apk/res/android\"", "xmlns=\"http://www.w3.org/2000/svg\"", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:pathData", "d", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:fillColor", "fill", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:strokeColor", "stroke", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:strokeWidth", "stroke-width", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:viewportHeight", "viewBox", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:viewportWidth", "viewBox", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:strokeAlpha", "stroke-opacity", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:fillAlpha", "fill-opacity", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:width", "width", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "android:height", "height", RegexOptions.IgnoreCase);
                svgContent = Regex.Replace(svgContent, "vector>", "svg>", RegexOptions.IgnoreCase);

                // 添加 viewBox 属性
                svgContent = Regex.Replace(svgContent, "viewBox=\"([^\"]*)\"", "viewBox=\"$1 0 0 $1\"", RegexOptions.IgnoreCase);

                // 将修改后的内容写入新的SVG文件
                string svgFilePath = Path.ChangeExtension(xmlFilePath, ".svg");
                File.WriteAllText(svgFilePath, svgContent, Encoding.UTF8);

                Console.WriteLine("Conversion successful. SVG file saved at: " + svgFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }
    }
}
