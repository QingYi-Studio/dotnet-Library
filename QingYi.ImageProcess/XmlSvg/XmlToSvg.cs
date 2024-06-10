using System.Text.RegularExpressions;
using System;
using System.IO;

namespace QingYi.ImageProcess.XmlSvg
{
    public class XmlToSvg
    {
        public string InputFilePath { get; set; } // 定义输入文件路径
        public string OutputFilePath { get; set; } // 定义输出文件路径
        public string DefaultFillColor { get; set; } // 定义默认填充颜色
        public string InputContent { get; set; } // 定义输入内容
        public string OutputContent { get; private set; } // 定义输出内容

        public string Convert()
        {
            if (!string.IsNullOrEmpty(InputFilePath) && !string.IsNullOrEmpty(OutputFilePath)) // 如果输入文件路径和输出文件路径都不为空
            {
                InputContent = File.ReadAllText(InputFilePath); // 读取输入文件内容
            }

            // 无输入文件，直接处理输入字符串
            if (string.IsNullOrEmpty(InputContent)) // 如果输入内容为空
            {
                throw new InvalidOperationException("Input content is empty."); // 抛出异常
            }

            // 颜色初始化
            if (string.IsNullOrEmpty(DefaultFillColor))
            {
                DefaultFillColor = "#ffffff";
            }

            OutputContent = ConvertXmlToSvg(InputContent, DefaultFillColor); // 将输入内容转换为SVG内容

            if (!string.IsNullOrEmpty(OutputFilePath)) // 如果输出文件路径不为空
            {
                File.WriteAllText(OutputFilePath, OutputContent); // 将输出内容写入文件
            }

            // 当输出文件路径为空是直接返回字符串
            return OutputContent;
        }

        private string ConvertXmlToSvg(string xmlContent, string defaultFillColor)
        {
            // 替换<?xml...>和<vector...>标签
            string svgContent = Regex.Replace(xmlContent, @"<\?xml[^\>]*\?>", "");
            svgContent = svgContent.Replace("<vector xmlns:android=\"http://schemas.android.com/apk/res/android\"", "<svg xmlns=\"http://www.w3.org/2000/svg\"");

            // 替换</vector>标签
            svgContent = svgContent.Replace("</vector>", "</svg>");

            // 替换android属性
            svgContent = svgContent.Replace("android:width", "width");
            svgContent = svgContent.Replace("android:height", "height");
            svgContent = svgContent.Replace("android:pathData", "d");
            svgContent = svgContent.Replace("android:fillColor", "fill");

            // 找到 android:viewportWidth 和 android:viewportHeight 的位置
            int viewportWidthIndex = svgContent.IndexOf("android:viewportWidth");
            int viewportHeightIndex = svgContent.IndexOf("android:viewportHeight");

            // 检查是否找到了 android:viewportWidth 和 android:viewportHeight
            if (viewportWidthIndex != -1 && viewportHeightIndex != -1)
            {
                // 使用正则表达式匹配 android:viewportWidth 和 android:viewportHeight 的值
                Match viewportWidthMatch = Regex.Match(svgContent, @"android:viewportWidth\s*=\s*""([\d.]+)""");
                Match viewportHeightMatch = Regex.Match(svgContent, @"android:viewportHeight\s*=\s*""([\d.]+)""");

                if (viewportWidthMatch.Success && viewportHeightMatch.Success)
                {
                    // 提取 android:viewportWidth 和 android:viewportHeight 的值
                    string viewportWidthValue = viewportWidthMatch.Groups[1].Value;
                    string viewportHeightValue = viewportHeightMatch.Groups[1].Value;

                    // 将 android:viewportWidth 和 android:viewportHeight 替换为 viewBox 属性
                    svgContent = Regex.Replace(svgContent, @"android:viewportWidth\s*=\s*""([\d.]+)""", $"viewBox=\"0 0 {viewportWidthValue} {viewportHeightValue}\"");
                    svgContent = Regex.Replace(svgContent, @"android:viewportHeight\s*=\s*""([\d.]+)""", "");
                }
            }

            // 替换描边相关属性
            svgContent = svgContent.Replace("android:strokeColor", "stroke");
            svgContent = svgContent.Replace("android:strokeWidth", "stroke-width");
            svgContent = svgContent.Replace("android:strokeAlpha", "stroke-opacity");
            svgContent = svgContent.Replace("android:fillAlpha", "fill-opacity");

            // 如果android:fillColor不存在，则添加默认填充颜色
            if (!svgContent.Contains("fill="))
            {
                svgContent = svgContent.Replace("<svg", "<svg fill=\"" + defaultFillColor + "\"");
            }

            return svgContent;
        }
    }
}