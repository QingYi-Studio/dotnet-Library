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
        public string OutputContent { get; set; }

        public string Convert()
        {
            string content = ReadContent();
            content = TransformContent(content);
            WriteContent(content);
            return content;
        }

        private string ReadContent()
        {
            // Read from file path if provided, otherwise from InputContent
            return !string.IsNullOrEmpty(InputFilePath) ? File.ReadAllText(InputFilePath) : InputContent;
        }

        private void WriteContent(string content)
        {
            // Write to file path if provided, otherwise to OutputContent
            if (!string.IsNullOrEmpty(OutputFilePath))
                File.WriteAllText(OutputFilePath, content);
            else
                OutputContent = content;
        }

        private string TransformContent(string content)
        {
            content = Regex.Replace(content, @"<svg xmlns=""http://www.w3.org/2000/svg""", @"<vector xmlns:android=""http://schemas.android.com/apk/res/android""");
            content = Regex.Replace(content, @"</svg>", @"</vector>");
            content = Regex.Replace(content, @"width", @"android:width");
            content = Regex.Replace(content, @"height", @"android:height");
            content = Regex.Replace(content, @" d=", @" android:pathData=");
            content = Regex.Replace(content, @" fill=""", @" android:fillColor=""");
            content = Regex.Replace(content, @"viewBox=""0 0 24 24""", @"android:viewportHeight=""24"" android:viewportWidth=""24""");
            content = Regex.Replace(content, @" stroke=""", @" android:strokeColor=""");
            content = Regex.Replace(content, @" stroke-width=", @" android:strokeWidth=");
            content = Regex.Replace(content, @" stroke-opacity=", @" android:strokeAlpha=");
            content = Regex.Replace(content, @" fill-opacity=", @" android:fillAlpha=");

            // Adding XML header if not present
            if (!content.StartsWith("<?xml version=\"1.0\" encoding=\"utf-8\"?>"))
                content = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + content;

            return content;
        }
    }
}
