Imports System.IO

Public Class Cracker

    Public AudioPath As String

    Dim generateLib As New GenerateLib

    ''' <summary>
    ''' Crack
    ''' </summary>
    Public Sub CrackAudio()
        Crack.CrackAudio(AudioPath)
    End Sub

    ''' <summary>
    ''' Initializes the core program. It cannot be used without initializing
    ''' </summary>
    Public Sub Initialize()
        generateLib.CreateLibFolder()
        generateLib.BuildNcmDump()
    End Sub

    ''' <summary>
    ''' [Be sure] to destroy the instance
    ''' </summary>
    Public Sub Destroy()
        Dim currentDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim libFolderPath As String = Path.Combine(currentDirectory, "lib")

        Try
            If Directory.Exists(libFolderPath) Then
                Directory.Delete(libFolderPath, True)
            End If
        Catch ex As Exception
            Throw New Exception($"删除 lib 文件夹时发生错误: {ex.Message}")
        End Try
    End Sub

End Class
