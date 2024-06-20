using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace QingYi.Core.Shell
{
    public class Bash
    {
        public string Command { get; set; }
        public bool UseShellExecute { get; set; } =false;
        public bool RedirectStandardOutput { get; set; } = true;
        public bool RedirectStandardError { get; set; } = true;
        public bool CreateNoWindow { get; set; } = true;
        public Encoding StandardOutputEncoding { get; set; } = Encoding.UTF8;
        public Encoding StandardErrorEncoding { get; set; } = Encoding.UTF8;

        // 异步执行 Bash 命令
        public async Task<string> ExecuteAsync()
        {
            var output = new StringBuilder();

            using (Process process = new Process())
            {
                ConfigureProcess(process);

                var tcs = new TaskCompletionSource<bool>();

                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
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

        // 同步执行 Bash 命令
        public string Execute()
        {
            var output = new StringBuilder();

            using (Process process = new Process())
            {
                ConfigureProcess(process);

                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        output.AppendLine(args.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

                return output.ToString();
            }
        }

        private void ConfigureProcess(Process process)
        {
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"{Command}\"";
            process.StartInfo.UseShellExecute = UseShellExecute;
            process.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
            process.StartInfo.RedirectStandardError = RedirectStandardError;
            process.StartInfo.CreateNoWindow = CreateNoWindow;
            process.StartInfo.StandardOutputEncoding = StandardOutputEncoding;
            process.StartInfo.StandardErrorEncoding = StandardErrorEncoding;
        }
    }
}
