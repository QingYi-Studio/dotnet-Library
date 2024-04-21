using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

namespace Image.AndroidXml2Svg
{
    public class Convert
    {
        public static void ConvertSync(string filePath)
        {
            try
            {
                // 1. 读取xml文件内容
                string xmlContent = File.ReadAllText(filePath);

                // 2. 替换<vector>为<svg>
                xmlContent = xmlContent.Replace("<vector", "<svg");

                // 3. 替换android:pathData为d
                xmlContent = xmlContent.Replace("android:pathData", "d");

                // 4. 替换android:fillColor为fill
                xmlContent = xmlContent.Replace("android:fillColor", "fill");

                // 5. 替换android:strokeColor为stroke
                xmlContent = xmlContent.Replace("android:strokeColor", "stroke");

                // 6. 替换android:strokeWidth为stroke-width
                xmlContent = xmlContent.Replace("android:strokeWidth", "stroke-width");

                // 7. 修改android:viewportHeight和android:viewportWidth为viewBox
                xmlContent = Regex.Replace(xmlContent, @"android:viewportHeight\s*=\s*""(\d+)""\s*android:viewportWidth\s*=\s*""(\d+)""", "viewBox=\"0 0 $2 $1\"");

                // 8. 替换android:strokeAlpha为stroke-opacity
                xmlContent = xmlContent.Replace("android:strokeAlpha", "stroke-opacity");

                // 9. 替换android:fillAlpha为fill-opacity
                xmlContent = xmlContent.Replace("android:fillAlpha", "fill-opacity");

                // 10. 替换android:width和android:height为width和height
                xmlContent = xmlContent.Replace("android:width", "width")
                xmlContent = xmlContent.Replace("android:height", "height");

                // 11. 替换xmlns:android为xmlns
                xmlContent = xmlContent.Replace("xmlns:android=\"http://schemas.android.com/apk/res/android\"", "xmlns=\"http://www.w3.org/2000/svg\"");

                // 12. 替换vector>为svg>
                xmlContent = xmlContent.Replace("vector>", "svg>");

                // 保存修改后的内容
                File.WriteAllText(filePath, xmlContent, Encoding.UTF8);

                Console.WriteLine("Conversion completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }
}
