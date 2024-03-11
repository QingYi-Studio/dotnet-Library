using System.Threading.Tasks.Dataflow;

namespace Tools.Shell.DataFlow
{
    public class DataFlow
    {
        public class Sync
        {
            private TransformBlock<string, string> cmdExecutorBlock;
            private TransformBlock<string, string> powerShellExecutorBlock;

            public Sync()
            {
                // 定义执行CMD命令的TransformBlock
                cmdExecutorBlock = new TransformBlock<string, string>(cmdCommand =>
                {
                    var output = ShellExecutor.Cmd.User.ExecuteCmdCommand(cmdCommand);
                    return output;
                });

                // 定义执行PowerShell命令的TransformBlock
                powerShellExecutorBlock = new TransformBlock<string, string>(psCommand =>
                {
                    var output = ShellExecutor.PowerShell.User.ExecutePowerShellCommand(psCommand);
                    return output;
                });
            }

            public string ExecuteCmdCommand(string cmdCommand)
            {
                cmdExecutorBlock.Post(cmdCommand);
                return cmdExecutorBlock.Receive();
            }

            public string ExecutePowerShellCommand(string psCommand)
            {
                powerShellExecutorBlock.Post(psCommand);
                return powerShellExecutorBlock.Receive();
            }
        }

        public class Async
        {
            private TransformBlock<string, string> cmdExecutorBlock; // 注意这里改为输出string
            private TransformBlock<string, string> powerShellExecutorBlock; // 注意这里改为输出string

            public Async()
            {
                // 定义异步执行CMD命令的TransformBlock
                cmdExecutorBlock = new TransformBlock<string, string>(async cmdCommand =>
                {
                    var output = await ShellExecutor.Cmd.User.ExecuteCmdCommandAsync(cmdCommand);
                    return output; // 这里直接返回string
                });

                // 定义异步执行PowerShell命令的TransformBlock
                powerShellExecutorBlock = new TransformBlock<string, string>(async psCommand =>
                {
                    var output = await ShellExecutor.PowerShell.User.ExecutePowerShellCommandAsync(psCommand);
                    return output; // 这里直接返回string
                });
            }

            public async Task<string> ExecuteCmdCommandAsync(string cmdCommand)
            {
                cmdExecutorBlock.Post(cmdCommand);
                return await cmdExecutorBlock.ReceiveAsync(); // 这里只需要一个await
            }

            public async Task<string> ExecutePowerShellCommandAsync(string psCommand)
            {
                powerShellExecutorBlock.Post(psCommand);
                return await powerShellExecutorBlock.ReceiveAsync(); // 这里只需要一个await
            }
        }
    }
    
    class T
    {
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
    }
}
