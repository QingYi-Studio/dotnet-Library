# WPF.MultiLanguage

<!--
这个类库是一个用于读取 JSON 文件并根据文件内容修改 WPF 窗口控件属性的工具。通过使用该类库，你可以轻松地将 JSON 文件中的数据绑定到 WPF 窗口中的控件，实现动态更新界面的效果。该类库提供了一个异步方法 `ReadJsonFileAsync`，它接受 JSON 文件的文件夹、文件名以及窗口实例作为参数，然后根据 JSON 文件内容来修改窗口中指定控件的属性值。

在 JSON 文件中，你可以定义不同类型的控件（比如 Label、Button 等），并指定它们的名称和需要修改的属性值。当你调用 `ReadJsonFileAsync` 方法时，类库将解析 JSON 文件，并根据文件内容找到对应的控件，然后修改其属性值。

这个类库的设计目的是使 WPF 应用程序能够更加灵活地根据外部数据动态更新界面，同时提高代码的复用性和可维护性。通过使用这个类库，你可以将界面的外观和内容分离，使得界面的修改变得更加简单和可配置。
-->

This library is a tool designed to read JSON files and modify WPF window control properties based on the content of the files. By using this library, you can easily bind data from JSON files to WPF window controls, enabling dynamic updates to the interface. The library provides an asynchronous method, `ReadJsonFileAsync`, which takes the folder path, file name of the JSON file, and an instance of the window as parameters. It then modifies the specified control properties in the window based on the content of the JSON file.

In the JSON file, you can define different types of controls (such as Label, Button, etc.), specify their names, and the property values that need to be modified. When you call the `ReadJsonFileAsync` method, the library will parse the JSON file, locate the corresponding controls based on the file content, and then update their property values.

The purpose of this library is to allow WPF applications to dynamically update their interface based on external data, improving flexibility while enhancing code reusability and maintainability. By using this library, you can separate the appearance and content of the interface, making it easier to modify the interface and configure it according to external data sources.

**Warn**: Now we only support button and label, if you want more, please send an issue on [GitHub](https://github.com/QingYi-Studio/CSharp-Library).

Attention:
- 1.0.0 and later is for .NET 8.0

## Usage

**MainWindow.caml.cs**

```c#
using WPF.MultiLanguage;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Cn_ClickAsync(object sender, RoutedEventArgs e)
        {
            await Translate.ReadJsonFileAsync("language", "cn", this);
        }
    }
}
```

**MainWindow.xaml**

```xaml
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Label x:Name="tlabel" Content="Label" HorizontalAlignment="Left" Margin="43,35,0,0" VerticalAlignment="Top"/>
        <Button x:Name="Cn" Content="cn" HorizontalAlignment="Left" Margin="43,117,0,0" VerticalAlignment="Top" Click="Cn_ClickAsync"/>
        <Button x:Name="En" Content="en" HorizontalAlignment="Left" Margin="128,127,0,0" VerticalAlignment="Top"/>
    </Grid>
</Window>
```

**Cn.json**

```json
{
  "Label": {
    "tlabel": "Welcome to My App"
  },
  "Button": {
    "Cn": "zw",
    "En": "yw"
  }
}
```
