Imports System.IO

Public Class StringBlock
    Private m_stringOffsets() As Integer
    Private m_strings() As Byte
    Private m_styleOffsets() As Integer
    Private m_styles() As Integer
    Private m_isUTF8 As Boolean = False
    Private Const CHUNK_TYPE As Integer = &H1C0001
    Private Const UTF8_FLAG As Integer = &H100

    Private ReadOnly UTF8_DECODER As Text.Decoder = Text.Encoding.UTF8.GetDecoder()
    Private ReadOnly UTF16LE_DECODER As Text.Decoder = Text.Encoding.Unicode.GetDecoder()

    Public Shared Function Read(reader As IntReader.IntReader) As StringBlock
        ChunkUtil.ChunkUtil.ReadCheckType(reader, CHUNK_TYPE)
        Dim chunkSize As Integer = reader.ReadInt()
        Dim stringCount As Integer = reader.ReadInt()
        Dim styleOffsetCount As Integer = reader.ReadInt()
        Dim flags As Integer = reader.ReadInt()
        Dim stringsOffset As Integer = reader.ReadInt()
        Dim stylesOffset As Integer = reader.ReadInt()

        Dim block As New StringBlock()
        block.m_isUTF8 = (flags And UTF8_FLAG) <> 0
        block.m_stringOffsets = reader.ReadIntArray(stringCount)
        If styleOffsetCount <> 0 Then
            block.m_styleOffsets = reader.ReadIntArray(styleOffsetCount)
        End If

        Dim size As Integer = If(stylesOffset = 0, chunkSize, stylesOffset) - stringsOffset
        block.m_strings = New Byte(size - 1) {}
        reader.ReadFully(block.m_strings)

        If stylesOffset <> 0 Then
            Dim sizeStyles As Integer = chunkSize - stylesOffset
            If (sizeStyles Mod 4) <> 0 Then
                Throw New IOException("Style data size is not multiple of 4 (" & sizeStyles & ").")
            End If
            block.m_styles = reader.ReadIntArray(sizeStyles \ 4)
        End If

        Return block
    End Function
End Class
