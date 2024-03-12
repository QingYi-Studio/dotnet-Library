Imports System.IO
Imports System.Threading.Tasks

Public Class Delete
    Public Shared Sub DeleteFilesAndFolders(paths As String(), Optional outputMessages As Boolean = False)
        For Each path In paths
            If Directory.Exists(path) Then
                Try
                    Directory.Delete(path, True)
                    If outputMessages Then
                        Console.WriteLine($"Successfully deleted folder: {path}")
                    End If
                Catch ex As Exception
                    If outputMessages Then
                        Console.WriteLine($"Failed to delete folder: {path}. Error: {ex.Message}")
                    End If
                End Try
            ElseIf File.Exists(path) Then
                Try
                    File.Delete(path)
                    If outputMessages Then
                        Console.WriteLine($"Successfully deleted file: {path}")
                    End If
                Catch ex As Exception
                    If outputMessages Then
                        Console.WriteLine($"Failed to delete file: {path}. Error: {ex.Message}")
                    End If
                End Try
            Else
                If outputMessages Then
                    Console.WriteLine($"Path not found: {path}")
                End If
            End If
        Next
    End Sub

    Public Shared Async Function DeleteFilesAndFoldersAsync(paths As String(), Optional outputMessages As Boolean = False) As Task
        Await Task.Run(Sub()
                           DeleteFilesAndFolders(paths, outputMessages)
                       End Sub)
    End Function
End Class
