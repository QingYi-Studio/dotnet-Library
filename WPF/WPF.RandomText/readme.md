# WPF.RandomText

A simple package to get random text from the file.

## Usage

```c#
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
```
