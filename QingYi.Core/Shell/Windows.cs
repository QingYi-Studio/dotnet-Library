using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace QingYi.Core.Shell
{
    public class Windows
    {
        public event EventHandler<string> OutputReceived; // 定义输出接收事件

        public bool CreateNoWindow { get; set; } = true;
        public string Command { get; set; }
        public bool UseShellExecute { get; set; } = false;
        public bool RedirectStandardOutput { get; set; } = true;
        public bool UsePowershell { get; set; } = false;
        public bool RunAsAdmin { get; set; } = false;

        // 同步执行命令
        public string ExecuteCommandSync()
        {
            var output = new StringBuilder();

            using (var process = new Process())
            {
                ConfigureProcess(process);

                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                        OnOutputReceived(args.Data); // 触发事件，通知输出
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                        OnOutputReceived(args.Data); // 触发事件，通知输出
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

                return output.ToString();
            }
        }

        // 异步执行命令
        public async Task<string> ExecuteCommandAsync()
        {
            var output = new StringBuilder();

            using (var process = new Process())
            {
                ConfigureProcess(process);

                var tcs = new TaskCompletionSource<bool>();

                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                        OnOutputReceived(args.Data); // 触发事件，通知输出
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                        OnOutputReceived(args.Data); // 触发事件，通知输出
                    }
                };

                process.Exited += (sender, args) =>
                {
                    tcs.TrySetResult(true);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await tcs.Task; // 等待进程退出

                return output.ToString();
            }
        }

        private void ConfigureProcess(Process process)
        {
            if (RunAsAdmin == true)
            {
                process.StartInfo.FileName = UseShellExecute ? "powershell.exe" : "cmd.exe";
                process.StartInfo.Arguments = UseShellExecute ? $"/c \"{Command}\"" : $"-Command \"{Command}\"";
                process.StartInfo.CreateNoWindow = CreateNoWindow;
                process.StartInfo.UseShellExecute = UseShellExecute;
                process.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
                process.StartInfo.RedirectStandardError = RedirectStandardOutput; // 也重定向错误输出以捕获可能的错误信息
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process.StartInfo.Verb = "runas";
            }
            else
            {
                process.StartInfo.FileName = UseShellExecute ? "powershell.exe" : "cmd.exe";
                process.StartInfo.Arguments = UseShellExecute ? $"/c \"{Command}\"" : $"-Command \"{Command}\"";
                process.StartInfo.CreateNoWindow = CreateNoWindow;
                process.StartInfo.UseShellExecute = UseShellExecute;
                process.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
                process.StartInfo.RedirectStandardError = RedirectStandardOutput; // 也重定向错误输出以捕获可能的错误信息
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            }
        }

        // 触发输出接收事件
        protected virtual void OnOutputReceived(string output)
        {
            OutputReceived?.Invoke(this, output);
        }
    }

    class WinUsage
    {
        static async Task Main(string[] args)
        {
            // 示例命令
            string command = "echo Hello, World!";

            // 创建 Windows 实例
            var windows = new Windows();
            windows.CreateNoWindow = true;
            windows.Command = command;
            windows.UseShellExecute = false; // 使用 true 表示使用 cmd.exe，false 表示使用 powershell.exe
            windows.RedirectStandardOutput = true;

            // 订阅输出接收事件
            windows.OutputReceived += (sender, output) =>
            {
                Console.WriteLine($"Received: {output}"); // 这里可以处理实时输出的内容
                                                          // 在这里将实时输出传递给主程序其他部分处理
            };

            // 同步执行命令
            Console.WriteLine("Executing command synchronously:");
            string syncOutput = windows.ExecuteCommandSync();
            Console.WriteLine("Sync Output:");
            Console.WriteLine(syncOutput);

            Console.WriteLine();

            // 异步执行命令
            Console.WriteLine("Executing command asynchronously:");
            string asyncOutput = await windows.ExecuteCommandAsync();
            Console.WriteLine("Async Output:");
            Console.WriteLine(asyncOutput);
        }
    }
}
