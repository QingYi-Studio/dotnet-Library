Class Crack

    Shared Sub CrackAudio(filePath As String)
        ' 创建 NeteaseCrypt 类的实例
        Dim neteaseCrypt As New Core(filePath)

        ' 启动转换过程
        Dim result As Integer = neteaseCrypt.Dump()

        ' 修复元数据
        neteaseCrypt.FixMetadata()

        ' [务必]销毁 NeteaseCrypt 类的实例
        neteaseCrypt.Destroy()
    End Sub

End Class
