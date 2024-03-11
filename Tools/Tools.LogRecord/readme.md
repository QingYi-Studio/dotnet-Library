# Tools.LogRecord

A library made by Qing-Yi Studio to make it easier to record logs or record the output.

Attention:
- 1.0.0 and later are for .NET 8.0

Usage:

```c#
void LogRecordExampleUsage()
{
    // Create a logger instance
    LogRecord logRecord = new LogRecord("logs");

    // single
    logRecord.WriteSingleLogFile("logs", "md5", "This is a debug message.", "Another debug message.");

    // user-defined
    logRecord.WriteCustomLogFile("CustomLogs", "custom.log", "none", "Custom log entry 1", "Custom log entry 2");
}

void OutputExample()
{
    Output.WriteToFile("output.txt", "This is a single line of output.");

    string multiLineContent = @"This is a
multi-line
output.";
    Output.WriteToFile("output.txt", multiLineContent);
}
```
