using System.IO;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Xml;

namespace WPF.MultiLanguage
{
    internal class Settings
    {
        readonly static string filename = "language.config";

        public static string? Language { get; set; }

        internal class CreateSettings
        {
            public static void Create(string language)
            {
                File.Create(filename);

                // 创建 XML 文档对象
                XmlDocument doc = new();

                // 创建 XML 声明
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(xmlDeclaration);

                // 创建根节点 <configuration>
                XmlElement root = doc.CreateElement("configuration");
                doc.AppendChild(root);

                // 创建 <language> 标签，并设置内容为 filename 变量的值
                XmlElement languageElement = doc.CreateElement("language");
                languageElement.InnerText = language;
                root.AppendChild(languageElement);

                // 保存 XML 文件
                doc.Save(filename);

                Console.WriteLine($"{filename} created and wrote data");
            }
        }

        internal class ReadLanguage
        {
            public static string Read(string createLanguage)
            {
                // 加载 XML 文件
                XmlDocument doc = new();
                doc.Load("language.config");  // 替换为你的 XML 文件路径

                // 获取 <language> 元素
                XmlNode languageNode = doc.SelectSingleNode("/configuration/language")!;

                if (languageNode != null)
                {
                    Language = languageNode.InnerText;
                    // Console.WriteLine("Language content: " + Language);
                    return Language;
                }
                else
                {
                    string error = "<language> element not found.";
                    Console.WriteLine(error);
                    CreateSettings.Create(createLanguage);
                    Language = error;
                    return Language;
                }
            }
        }
    }
}
