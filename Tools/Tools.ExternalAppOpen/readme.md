# Tools.ExternalAppOpen

A small library for opening files using external applications.

Suitable for .NET 6 to .NET 8.

## Usage

```c#
using Tools.ExternalAppOpen;

ExternalAppOpen appOpen = new ExternalAppOpen();

// user open
string filePath = "C:\\path\\to\\file.txt";
string appName = "notepad.exe";
appOpen.OpenFileWithApp(filePath, appName);

// admin open
string adminFilePath = "C:\\path\\to\\adminFile.txt";
string adminAppName = "notepad.exe";
appOpen.OpenFileWithAppAsAdmin(adminFilePath, adminAppName);
```
