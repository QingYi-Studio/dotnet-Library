namespace Tools.Shell.Async
{
    public class ShellExecutorAsync
    {
        public class Cmd
        {
            public class User
            {
                public static Task<string> ExecuteCmdCommandAsync(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    return Task.Run(() => ShellExecutor.Cmd.User.ExecuteCmdCommand(command, redirectOutput, useShellExecute, createNoWindow));
                }
            }
            public class Admin
            {
                public static Task<string> ExecuteCmdCommandAsync(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    return Task.Run(() => ShellExecutor.Cmd.Admin.ExecuteCmdCommand(command, redirectOutput, useShellExecute, createNoWindow));
                }
            }
        }
        public class PowerShell
        {
            public class User
            {
                public static async Task<string> ExecutePowerShellCommandAsync(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    return await Task.Run(() => ShellExecutor.PowerShell.User.ExecutePowerShellCommand(command, redirectOutput, useShellExecute, createNoWindow));
                }
            }
            public class Admin
            {
                public static async Task<string> ExecutePowerShellCommandAsAdminAsync(string command, bool redirectOutput = true, bool useShellExecute = false, bool createNoWindow = true)
                {
                    return await Task.Run(() => ShellExecutor.PowerShell.Admin.ExecutePowerShellCommandAsAdmin(command, redirectOutput, useShellExecute, createNoWindow));
                }
            }
        }
    }
}
