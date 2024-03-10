# Tools.Shell

A library made by Qing-Yi studio to make it easier to execute Cmd or PowerShell commands.

Attention:
- 1.0.0-1.1.0 is for .NET 6.0
- 1.2.0 is for .NET 7.0
- 1.3.0 and later is for .NET 8.0

Usage:

```c#
static void CommandFuncSync(string[] args)
{
	ShellExecutor.Cmd.User.ExecuteCmdCommand(cmdcommand);
	ShellExecutor.PowerShell.User.ExecutePowerShellCommand(pscommand);
}
```

```c#
static async void CommandFuncAsync(string[] args)
{
	await ShellExecutor.Cmd.User.ExecuteCmdCommandAsync(cmdcommand);
	await ShellExecutor.PowerShell.User.ExecutePowerShellCommandAsync(pscommand);
}
```

