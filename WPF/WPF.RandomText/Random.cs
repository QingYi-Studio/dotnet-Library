using System.IO;

namespace WPF.RandomText
{
    public class TextSelector
    {
        private string filePath;

        public TextSelector(string filePath)
        {
            this.filePath = filePath;
        }

        public string SelectText(string mode)
        {
            if (mode == "line")
            {
                string[] lines = File.ReadAllLines(filePath);
                Random rnd = new();
                int randomLineNumber = rnd.Next(0, lines.Length);
                return lines[randomLineNumber];
            }
            else
            {
                string text = File.ReadAllText(filePath);
                string[]? parts = null;
                if (mode == "semicolon")
                {
                    parts = text.Split(';', '\n');
                }
                else if (mode == "comma")
                {
                    parts = text.Split(',', '\n');
                }
                else if (mode == "pipe")
                {
                    parts = text.Split('|', '\n');
                }
                else if (mode == "backslash")
                {
                    parts = text.Split('\\', '\n');
                }
                else if (mode == "slash")
                {
                    parts = text.Split('/', '\n');
                }
                else if (mode == "exclamation")
                {
                    parts = text.Split('!', '\n');
                }
                else
                {
                    return "无效的模式选择。请选择 'line'、'semicolon'、'comma'、'pipe'、'backslash'、'slash' 或 'exclamation'。";
                }
                
                // 去除空白部分
                parts = Array.FindAll(parts, p => !string.IsNullOrWhiteSpace(p));

                Random rnd = new();
                int randomPartIndex = rnd.Next(0, parts.Length);
                return parts[randomPartIndex];
            }
        }
    }

    class UUU
    {
        static void Main()
        {
            string filePath = "your_file_path_here.txt"; // 替换为你的文件路径

            TextSelector textSelector = new(filePath);

            // 选择行模式
            string selectedLine = textSelector.SelectText("line");
            Console.WriteLine("Selected Line: " + selectedLine);

            // 选择分号模式
            string selectedSemicolonPart = textSelector.SelectText("semicolon");
            Console.WriteLine("Selected Semicolon Part: " + selectedSemicolonPart);

            // 选择逗号模式
            string selectedCommaPart = textSelector.SelectText("comma");
            Console.WriteLine("Selected Comma Part: " + selectedCommaPart);
        }
    }
}
