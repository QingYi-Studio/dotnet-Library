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
End Class
