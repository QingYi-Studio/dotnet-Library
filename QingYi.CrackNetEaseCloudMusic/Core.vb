Imports System.Runtime.InteropServices

Class Core

    Private Const DLL_PATH As String = "lib/libncmdump.dll"
    Private ReadOnly NeteaseCryptClass As IntPtr = IntPtr.Zero

    <DllImport(DLL_PATH, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function CreateNeteaseCrypt(path As IntPtr) As IntPtr
    End Function

    <DllImport(DLL_PATH, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function Dump(NeteaseCrypt As IntPtr) As Integer
    End Function

    <DllImport(DLL_PATH, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Sub FixMetadata(NeteaseCrypt As IntPtr)
    End Sub

    <DllImport(DLL_PATH, CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Sub DestroyNeteaseCrypt(NeteaseCrypt As IntPtr)
    End Sub

    ''' <summary>
    ''' 创建 NeteaseCrypt 类的实例。
    ''' </summary>
    ''' <param name="FileName">网易云音乐 ncm 加密文件路径</param>
    Sub New(FileName As String)
        NeteaseCryptClass = CreateNeteaseCrypt(Marshal.StringToHGlobalAnsi(FileName))
    End Sub

    ''' <summary>
    ''' 启动转换过程。
    ''' </summary>
    ''' <returns>返回一个整数，指示转储过程的结果。如果成功，返回0；如果失败，返回1。</returns>
    Function Dump() As Integer
        Return Dump(NeteaseCryptClass)
    End Function

    ''' <summary>
    ''' 修复音乐文件元数据。
    ''' </summary>
    Sub FixMetadata()
        FixMetadata(NeteaseCryptClass)
    End Sub

    ''' <summary>
    ''' 销毁 NeteaseCrypt 类的实例。
    ''' </summary>
    Sub Destroy()
        DestroyNeteaseCrypt(NeteaseCryptClass)
    End Sub

End Class
