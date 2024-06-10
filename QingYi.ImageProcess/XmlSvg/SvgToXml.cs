using System;
using System.IO;
using System.Text.RegularExpressions;

namespace QingYi.ImageProcess.XmlSvg
{
    public class SvgToXml
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public string InputContent { get; set; }
        public string OutputContent { get; private set; }

        public string Convert()
        {
            if (!string.IsNullOrEmpty(InputFilePath))
            {
                InputContent = File.ReadAllText(InputFilePath);
            }

            if (string.IsNullOrEmpty(InputContent))
            {
                throw new InvalidOperationException("Input content is empty.");
            }

            string xmlContent = ReplaceSvgTags(InputContent);
            if (!string.IsNullOrEmpty(OutputFilePath))
            {
                File.WriteAllText(OutputFilePath, xmlContent);
            }
            else
            {
                OutputContent = xmlContent;
            }

            return xmlContent;
        }

        private string ReplaceSvgTags(string svgContent)
        {
            string xmlContent = svgContent;

            // 替换 <svg xmlns="http://www.w3.org/2000/svg" 为 <vector xmlns:android="http://schemas.android.com/apk/res/android"
            xmlContent = xmlContent.Replace("<svg xmlns=\"http://www.w3.org/2000/svg\"", "<vector xmlns:android=\"http://schemas.android.com/apk/res/android\"");

            // 替换 </svg> 为 </vector>
            xmlContent = xmlContent.Replace("</svg>", "</vector>");

            // 替换 width 为 android:width
            xmlContent = xmlContent.Replace("width", "android:width");

            // 替换 height 为 android:height
            xmlContent = xmlContent.Replace("height", "android:height");

            // 替换 d 为 android:pathData
            xmlContent = xmlContent.Replace("d", "android:pathData");

            // 替换 fill 为 android:fillColor
            xmlContent = xmlContent.Replace("fill", "android:fillColor");

            // 替换 viewBox="0 0 24 24" 为 android:viewportHeight="24" android:viewportWidth="24"
            xmlContent = Regex.Replace(xmlContent, @"viewBox=""([\d\s.]+)""", m =>
            {
                string[] values = m.Groups[1].Value.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 4)
                {
                    return $"android:viewportHeight=\"{values[3]}\" android:viewportWidth=\"{values[2]}\"";
                }
                else
                {
                    return m.Value;
                }
            });

            // 替换 stroke 为 android:strokeColor
            xmlContent = xmlContent.Replace("stroke", "android:strokeColor");

            // 替换 stroke-width 为 android:strokeWidth
            xmlContent = xmlContent.Replace("stroke-width", "android:strokeWidth");

            // 替换 stroke-opacity 为 android:strokeAlpha
            xmlContent = xmlContent.Replace("stroke-opacity", "android:strokeAlpha");

            // 替换 fill-opacity 为 android:fillAlpha
            xmlContent = xmlContent.Replace("fill-opacity", "android:fillAlpha");

            return xmlContent;
        }
    }
}
