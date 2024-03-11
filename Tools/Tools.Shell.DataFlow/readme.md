# Tools.Shell.DataFlow

A subcomponent of the Tools.Shell module that uses DataFlow to read Shell output.

Attention:
- 1.0.0 and later are for .NET 8.0

Usage:

```c#
void Sync()
{
    // 创建 SyncShellExecutorDataflow 实例
    var shellExecutor = new DataFlow.Sync();

    // 定义你想要执行的CMD命令
    string cmdCommand = "echo Hello from CMD";
    // 使用 SyncShellExecutorDataflow 执行CMD命令并获取输出
    string cmdOutput = shellExecutor.ExecuteCmdCommand(cmdCommand);
    Console.WriteLine("CMD Output: " + cmdOutput);

    // 定义你想要执行的PowerShell命令
    string psCommand = "Write-Output 'Hello from PowerShell'";
    // 使用 SyncShellExecutorDataflow 执行PowerShell命令并获取输出
    string psOutput = shellExecutor.ExecutePowerShellCommand(psCommand);
    Console.WriteLine("PowerShell Output: " + psOutput);
}

async void Async()
{
    // 创建 AsyncShellExecutorDataflow 实例
    var shellExecutor = new DataFlow.Async();

    // 定义你想要执行的CMD命令
    string cmdCommand = "echo Hello from CMD";
    // 使用 AsyncShellExecutorDataflow 执行CMD命令并获取输出
    string cmdOutput = await shellExecutor.ExecuteCmdCommandAsync(cmdCommand);
    Console.WriteLine("CMD Output: " + cmdOutput);

    // 定义你想要执行的PowerShell命令
    string psCommand = "Write-Output 'Hello from PowerShell'";
    // 使用 AsyncShellExecutorDataflow 执行PowerShell命令并获取输出
    string psOutput = await shellExecutor.ExecutePowerShellCommandAsync(psCommand);
    Console.WriteLine("PowerShell Output: " + psOutput);
}
```
