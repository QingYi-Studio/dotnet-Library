using WPF.MultiLanguage;
using System.Windows;

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
            // 假设在 MainWindow 类中调用
            await Translate.ReadJsonFileAsync("language", "cn", this);
        }
    }
}