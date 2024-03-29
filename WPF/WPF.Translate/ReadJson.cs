using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace WPF.MultiLanguage
{
    public class Translate
    {
        public static async Task ChangeLanguageAsync(string folder, string fileName, Window window)
        {
            string filePath = "language.config";

            if (File.Exists(filePath))
            {
                string Language = Settings.ReadLanguage.Read(fileName);
                // Console.WriteLine(Language);
                await ReadJsonFileFastAsync(folder, Language, window);
            }
            else
            {
                Settings.CreateSettings.Create(fileName);
                await ReadJsonFileAsync(folder, fileName, window);
            }
        }
        private static async Task ReadJsonFileAsync(string folder, string fn, Window window)
        {
            string language = Settings.ReadLanguage.Read(fn);
            string jsonFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folder);
            string jsonFilePath = Path.Combine(jsonFolderPath, $"{language}.json");

            if (File.Exists(jsonFilePath))
            {
                string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                using JsonDocument document = JsonDocument.Parse(jsonContent);

                JsonElement root = document.RootElement;

                ApplyPropertiesToControls(root, "Label", window, (control, propertyValue) =>
                {
                    if (control is Label label)
                    {
                        label.Content = propertyValue;
                    }
                });

                ApplyPropertiesToControls(root, "Button", window, (control, propertyValue) =>
                {
                    if (control is Button button)
                    {
                        button.Content = propertyValue;
                    }
                });
            }
            else
            {
                Console.WriteLine("Json File NotFound");
            }
        }

        private static async Task ReadJsonFileFastAsync(string folder, string lan, Window window)
        {
            string jsonFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folder);
            string jsonFilePath = Path.Combine(jsonFolderPath, $"{lan}.json");

            if (File.Exists(jsonFilePath))
            {
                string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                using JsonDocument document = JsonDocument.Parse(jsonContent);

                JsonElement root = document.RootElement;

                ApplyPropertiesToControls(root, "Label", window, (control, propertyValue) =>
                {
                    if (control is Label label)
                    {
                        label.Content = propertyValue;
                    }
                });

                ApplyPropertiesToControls(root, "Button", window, (control, propertyValue) =>
                {
                    if (control is Button button)
                    {
                        button.Content = propertyValue;
                    }
                });
            }
            else
            {
                Console.WriteLine("Json File NotFound");
            }
        }

        private static void ApplyPropertiesToControls(JsonElement root, string sectionName, Window window, Action<Control, string> action)
        {
            if (root.TryGetProperty(sectionName, out JsonElement sectionObject))
            {
                foreach (JsonProperty property in sectionObject.EnumerateObject())
                {
                    string propertyName = property.Name;
                    string? propertyValue = property.Value.GetString();

                    Control? control = window.FindName(propertyName) as Control;
                    if (control != null)
                    {
                        action(control, propertyValue!);
                    }
                }
            }
        }
    }
}
