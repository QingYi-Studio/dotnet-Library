using System.Text.RegularExpressions;

namespace Picture.AXmlToSvg
{
    internal class Convert
    {
        /// <summary>
        /// 同步转换
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static string ConvertSync(string filePath)
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
                xmlContent = Regex.Replace(xmlContent, @"android:viewportWidth\s*=\s*""(\d+(\.\d+)?)""\s*android:viewportHeight\s*=\s*""(\d+(\.\d+)?)""", "viewBox=\"0 0 $1 $3\"");

                // 8. 替换android:strokeAlpha为stroke-opacity
                xmlContent = xmlContent.Replace("android:strokeAlpha", "stroke-opacity");

                // 9. 替换android:fillAlpha为fill-opacity
                xmlContent = xmlContent.Replace("android:fillAlpha", "fill-opacity");

                // 10. 替换android:width和android:height为width和height
                xmlContent = xmlContent.Replace("android:width", "width");
                xmlContent = xmlContent.Replace("android:height", "height");

                // 11. 替换xmlns:android为xmlns
                xmlContent = xmlContent.Replace("xmlns:android=\"http://schemas.android.com/apk/res/android\"", "xmlns=\"http://www.w3.org/2000/svg\"");

                // 12. 替换vector>为svg>
                xmlContent = xmlContent.Replace("vector>", "svg>");

                // 返回修改后的内容
                return xmlContent;
            }
            catch (Exception ex)
            {
                return $"Error during conversion: {ex.Message}";
            }
        }

        /// <summary>
        /// 异步转换
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static async Task<string> ConvertAsync(string filePath)
        {
            try
            {
                // 1. 读取xml文件内容
                string xmlContent = await File.ReadAllTextAsync(filePath);

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
                xmlContent = Regex.Replace(xmlContent, @"android:viewportWidth\s*=\s*""(\d+(\.\d+)?)""\s*android:viewportHeight\s*=\s*""(\d+(\.\d+)?)""", "viewBox=\"0 0 $1 $3\"");

                // 8. 替换android:strokeAlpha为stroke-opacity
                xmlContent = xmlContent.Replace("android:strokeAlpha", "stroke-opacity");

                // 9. 替换android:fillAlpha为fill-opacity
                xmlContent = xmlContent.Replace("android:fillAlpha", "fill-opacity");

                // 10. 替换android:width和android:height为width和height
                xmlContent = xmlContent.Replace("android:width", "width").Replace("android:height", "height");

                // 11. 替换xmlns:android为xmlns
                xmlContent = xmlContent.Replace("xmlns:android=\"http://schemas.android.com/apk/res/android\"", "xmlns=\"http://www.w3.org/2000/svg\"");

                // 12. 替换vector>为svg>
                xmlContent = xmlContent.Replace("vector>", "svg>");

                // 返回修改后的内容
                return xmlContent;
            }
            catch (Exception ex)
            {
                return $"Error during conversion: {ex.Message}";
            }
        }
    }
}
