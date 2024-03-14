using System.Diagnostics;

namespace Tools.Shell
{
    public class ShellExecutor
    {
        public class Cmd
        {
            public class User
            {
                public static string ExecuteCmdCommand(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    string output;
                    try
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo();
                        processStartInfo.FileName = "cmd.exe"; // Windows系统使用的cmd
                        processStartInfo.Arguments = "/c " + command;

                        // 如果将此属性设置为 true，则可以通过 StandardOutput 属性从进程中获取输出流
                        // 这在需要捕获命令执行结果时非常有用
                        // 如果设置为 false，则无法通过 StandardOutput 属性获取输出流
                        processStartInfo.RedirectStandardOutput = redirectOutput;

                        // 是否使用操作系统外壳来启动进程
                        // 如果将此属性设置为 true，则使用操作系统外壳启动进程；如果设置为 false，则不使用操作系统外壳启动进程
                        // 通常情况下，当设置为 false 时，可以更好地控制进程的启动和交互
                        processStartInfo.UseShellExecute = useShellExecute;

                        // 是否在启动进程时创建一个新窗口
                        // 如果将此属性设置为 true，则不会创建进程的新窗口；如果设置为 false，则会创建新窗口
                        // 通常在后台执行命令时将其设置为 true 可以确保不会弹出命令行窗口。
                        processStartInfo.CreateNoWindow = createNoWindow;

                        Process process = new Process();
                        process.StartInfo = processStartInfo;
                        process.Start();

                        output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        output = ex.Message;
                    }

                    return output;
                }
            }
            public class Admin
            {
                public static string ExecuteCmdCommand(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    string output;
                    try
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo();
                        processStartInfo.FileName = "cmd.exe"; // Windows系统使用的cmd
                        processStartInfo.Arguments = "/c " + command;
                        processStartInfo.Verb = "runas"; // 以管理员权限运行

                        // 如果将此属性设置为 true，则可以通过 StandardOutput 属性从进程中获取输出流
                        // 这在需要捕获命令执行结果时非常有用
                        // 如果设置为 false，则无法通过 StandardOutput 属性获取输出流
                        processStartInfo.RedirectStandardOutput = redirectOutput;

                        // 是否使用操作系统外壳来启动进程
                        // 如果将此属性设置为 true，则使用操作系统外壳启动进程；如果设置为 false，则不使用操作系统外壳启动进程
                        // 通常情况下，当设置为 false 时，可以更好地控制进程的启动和交互
                        processStartInfo.UseShellExecute = useShellExecute;

                        // 是否在启动进程时创建一个新窗口
                        // 如果将此属性设置为 true，则不会创建进程的新窗口；如果设置为 false，则会创建新窗口
                        // 通常在后台执行命令时将其设置为 true 可以确保不会弹出命令行窗口。
                        processStartInfo.CreateNoWindow = createNoWindow;

                        Process process = new Process();
                        process.StartInfo = processStartInfo;
                        process.Start();

                        output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        output = ex.Message;
                    }

                    return output;
                }
            }
        }
        public class PowerShell
        {
            public class User
            {
                public static string ExecutePowerShellCommand(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    string output;
                    try
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo();
                        processStartInfo.FileName = "powershell";
                        processStartInfo.Arguments = $"-NoProfile -ExecutionPolicy unrestricted -Command \"{command}\"";
                        processStartInfo.RedirectStandardOutput = redirectOutput;
                        processStartInfo.UseShellExecute = useShellExecute;
                        processStartInfo.CreateNoWindow = createNoWindow;

                        Process process = new Process();
                        process.StartInfo = processStartInfo;
                        process.Start();

                        output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        output = ex.Message;
                    }

                    return output;
                }
            }

            public class Admin
            {
                public static string ExecutePowerShellCommandAsAdmin(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    string output;
                    try
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo();
                        processStartInfo.FileName = "powershell";
                        processStartInfo.Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"";
                        processStartInfo.Verb = "runas"; // Run as administrator
                        processStartInfo.RedirectStandardOutput = redirectOutput;
                        processStartInfo.UseShellExecute = useShellExecute;
                        processStartInfo.CreateNoWindow = createNoWindow;

                        Process process = new Process();
                        process.StartInfo = processStartInfo;
                        process.Start();

                        output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        output = ex.Message;
                    }

                    return output;
                }
            }
        }
    }
}