using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace QingYi.Core.Shell
{
    public class Bash
    {
        public event EventHandler<string> OutputReceived;

        public bool CreateNoWindow { get; set; } = true;
        public string Command { get; set; }
        public bool UseShellExecute { get; set; } = false;
        public bool RedirectStandardOutput { get; set; }

        public Bash()
        {
            // Default settings
            CreateNoWindow = true;
            UseShellExecute = false;
            RedirectStandardOutput = true;
        }

        public async Task<string> ExecuteAsync()
        {
            StringBuilder output = new StringBuilder();

            using (Process process = new Process())
            {
                ConfigureProcess(process);

                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        output.AppendLine(e.Data);
                        OnOutputReceived(e.Data);
                    }
                };

                process.Start();

                // Begin asynchronous read of stdout
                process.BeginOutputReadLine();

                await process.WaitForExitAsync();

                return output.ToString();
            }
        }

        public string Execute()
        {
            StringBuilder output = new StringBuilder();

            using (Process process = new Process())
            {
                ConfigureProcess(process);

                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        output.AppendLine(e.Data);
                        OnOutputReceived(e.Data);
                    }
                };

                process.Start();

                // Synchronously read stdout
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    output.AppendLine(line);
                    OnOutputReceived(line);
                }

                process.WaitForExit();

                return output.ToString();
            }
        }

        private void ConfigureProcess(Process process)
        {
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"{Command}\"";
            process.StartInfo.CreateNoWindow = CreateNoWindow;
            process.StartInfo.UseShellExecute = UseShellExecute;
            process.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
        }

        private void OnOutputReceived(string data)
        {
            OutputReceived?.Invoke(this, data);
        }
    }

    // Extension method to await Process.WaitForExit asynchronously
    public static class ProcessExtensions
    {
        public static Task WaitForExitAsync(this Process process)
        {
            var tcs = new TaskCompletionSource<bool>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(true);
            return tcs.Task;
        }
    }

    class BashUsage
    {
        static async Task Use(string[] args)
        {
            Bash bash = new Bash();
            bash.Command = "echo Hello World!; sleep 1; echo Bye!";

            // 异步执行，实时获取输出
            bash.OutputReceived += (sender, output) =>
            {
                Console.WriteLine(output); // 实时输出到控制台示例
                                           // 在这里可以处理输出，比如存储到变量或者文件中
            };

            string result = await bash.ExecuteAsync();
            Console.WriteLine($"Final output:\n{result}");

            // 同步执行示例
            // string resultSync = bash.Execute();
            // Console.WriteLine($"Final output:\n{resultSync}");
        }
    }
}
