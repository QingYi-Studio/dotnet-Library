# Tools.Shell.Async

Run Tools.Shell asynchronously.

Attention:
- 1.0.0 and later is for .NET 8.0

## Usage

### Cmd

#### User

```c#
string command = "your_command_here";
string result = await ShellExecutorAsync.Cmd.User.ExecuteCmdCommandAsync(command);
Console.WriteLine(result);
```

#### Admin

```c#
string command = "your_command_here";
string result = await ShellExecutorAsync.Cmd.Admin.ExecuteCmdCommandAsync(command);
Console.WriteLine(result);
```

### PowerShell

#### User

```c#
string command = "your_command_here";
string result = await ShellExecutorAsync.PowerShell.User.ExecuteCmdCommandAsync(command);
Console.WriteLine(result);
```

#### Admin

```c#
string command = "your_command_here";
string result = await ShellExecutorAsync.PowerShell.Admin.ExecuteCmdCommandAsync(command);
Console.WriteLine(result);
```
