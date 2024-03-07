# Tools.Shell

A library made by Qing-Yi studio to make it easier to execute Cmd or PowerShell commands.

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

