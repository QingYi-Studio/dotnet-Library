using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace QingYi.Core.Shell
{
    public class Windows
    {
        private readonly ProcessStartInfo _processInfo;

        public Windows()
        {
            _processInfo = new ProcessStartInfo();
        }

        public string Command
        {
            get { return _processInfo.Arguments; }
            set { _processInfo.Arguments = value; }
        }

        public bool CreateNoWindow
        {
            get { return _processInfo.CreateNoWindow; }
            set { _processInfo.CreateNoWindow = value; }
        }

        public bool UseShellExecute
        {
            get { return _processInfo.UseShellExecute; }
            set { _processInfo.UseShellExecute = value; }
        }

        public bool RedirectStandardOutput
        {
            get { return _processInfo.RedirectStandardOutput; }
            set { _processInfo.RedirectStandardOutput = value; }
        }

        public string ExecuteCmdCommandSync()
        {
            _processInfo.FileName = "cmd.exe";
            return ExecuteCommandSync();
        }

        public async Task<string> ExecuteCmdCommandAsync()
        {
            _processInfo.FileName = "cmd.exe";
            return await ExecuteCommandAsync();
        }

        public string ExecutePowerShellCommandSync()
        {
            _processInfo.FileName = "powershell.exe";
            return ExecuteCommandSync();
        }

        public async Task<string> ExecutePowerShellCommandAsync()
        {
            _processInfo.FileName = "powershell.exe";
            return await ExecuteCommandAsync();
        }

        private string ExecuteCommandSync()
        {
            string output = "";

            try
            {
                _processInfo.RedirectStandardError = true;
                using (Process process = Process.Start(_processInfo))
                {
                    if (process != null)
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            output = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        output = "Failed to start process.";
                    }
                }
            }
            catch (Exception ex)
            {
                output = $"Error executing command: {ex.Message}";
            }

            return output;
        }

        private async Task<string> ExecuteCommandAsync()
        {
            string output = "";

            try
            {
                _processInfo.RedirectStandardError = true;
                using (Process process = Process.Start(_processInfo))
                {
                    if (process != null)
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            output = await reader.ReadToEndAsync();
                        }
                    }
                    else
                    {
                        output = "Failed to start process.";
                    }
                }
            }
            catch (Exception ex)
            {
                output = $"Error executing command asynchronously: {ex.Message}";
            }

            return output;
        }
    }
}
