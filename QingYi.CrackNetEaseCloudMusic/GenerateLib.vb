Imports System.IO
Imports System.Resources

Public Class GenerateLib

    ''' <summary>
    ''' Get libncmdump.dll base64 string
    ''' </summary>
    ''' <param name="resourceName"></param>
    ''' <returns></returns>
    Function GetResourceString(resourceName As String) As String
        Dim assembly As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim resourceManager As New ResourceManager("GenerateLib.Resources", assembly)

        Return resourceManager.GetString(resourceName)
    End Function

    ''' <summary>
    ''' Build 
    ''' </summary>
    ''' <param name="base64Text">Base 64 string</param>
    ''' <param name="filePath">Output file path</param>
    Sub Builder(base64Text As String, filePath As String)
        ' 解码 Base64 文本为二进制数据
        Dim bytes As Byte() = Convert.FromBase64String(base64Text)

        Try
            ' 确保文件夹存在，如果不存在则创建
            Dim directoryPath As String = Path.GetDirectoryName(filePath)
            If Not Directory.Exists(directoryPath) Then
                Directory.CreateDirectory(directoryPath)
            End If

            ' 创建空文件并关闭文件流
            File.Create(filePath).Close()

            ' 写入二进制数据到文件
            File.WriteAllBytes(filePath, bytes)
        Catch ex As Exception
            Throw New IOException("Error: " & ex.Message)
        End Try
    End Sub
End Class
