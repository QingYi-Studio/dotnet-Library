# Tools.Delete.Async

An asynchronous version of the Tools.Delete package that allows for asynchronous deletion.

Attention:
- 1.0.0 and later is for .NET 8.0

Usage:

```c#
string[] pathsToDelete = { "path/to/delete/file1.txt", "path/to/delete/folder2", "path/to/delete/*.txt", "path/to/delete/text.*" };
AsyncDelete.DeleteFilesAndFoldersAsync(pathsToDelete, outputMessages: true).Wait();
AsyncDelete.ForceDeleteAsync(pathsToDelete, outputMessages: true).Wait();
```
