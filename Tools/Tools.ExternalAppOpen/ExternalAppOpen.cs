using System.Diagnostics;

namespace Tools.ExternalAppOpen
{
    public class ExternalAppOpen
    {
        public void OpenFileWithApp(string filePath, string appName, params string[] additionalArgs)
        {
            OpenFile(filePath, appName, false, additionalArgs);
        }

        public void OpenFileWithAppAsAdmin(string filePath, string appName, params string[] additionalArgs)
        {
            OpenFile(filePath, appName, true, additionalArgs);
        }

        private static void OpenFile(string filePath, string appName, bool runAsAdmin, params string[] additionalArgs)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = appName;
                startInfo.Arguments = $"\"{filePath}\" {string.Join(" ", additionalArgs)}";
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.CreateNoWindow = false;

                if (runAsAdmin)
                {
                    startInfo.Verb = "runas";
                }

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
