# Tools.Delete

A simple package optimizes natively deleted code to make it easier to use.

Attention:
- 1.0.0 and later is for .NET 8.0

Usage:

```c#
string[] pathsToDelete = { "path/to/delete/file1.txt", "path/to/delete/folder2", "path/to/delete/*.txt", "path/to/delete/text.*" };
Delete.DeleteFilesAndFolders(pathsToDelete, outputMessages: true);
```
