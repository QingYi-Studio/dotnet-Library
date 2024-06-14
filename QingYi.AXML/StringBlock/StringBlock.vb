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

        Dim block As New StringBlock With {
            .m_isUTF8 = (flags And UTF8_FLAG) <> 0,
            .m_stringOffsets = reader.ReadIntArray(stringCount)
        }
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

    Public Function GetString(index As Integer) As String
        If index < 0 OrElse m_stringOffsets Is Nothing OrElse index >= m_stringOffsets.Length Then
            Return Nothing
        End If

        Dim offset As Integer = m_stringOffsets(index)
        Dim length As Integer
        Dim val As Integer()

        If m_isUTF8 Then
            val = GetUtf8(m_strings, offset)
            offset = val(0)
        Else
            val = GetUtf16(m_strings, offset)
            offset += val(0)
        End If

        length = val(1)
        Return DecodeString(offset, length)
    End Function

    Private Function DecodeString(offset As Integer, length As Integer) As String
        Try
            If m_isUTF8 Then
                Dim charCount As Integer = UTF8_DECODER.GetCharCount(m_strings, offset, length)
                Dim chars(charCount - 1) As Char
                UTF8_DECODER.GetChars(m_strings, offset, length, chars, 0)
                Return New String(chars)
            Else
                Dim charCount As Integer = UTF16LE_DECODER.GetCharCount(m_strings, offset, length)
                Dim chars(charCount - 1) As Char
                UTF16LE_DECODER.GetChars(m_strings, offset, length, chars, 0)
                Return New String(chars)
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Shared Function GetShort(array As Byte(), offset As Integer) As Integer
        Return ((array(offset + 1) And &HFF) << 8) Or (array(offset) And &HFF)
    End Function

    Private Shared Function GetUtf8(array As Byte(), offset As Integer) As Integer()
        Dim val As Integer = array(offset)
        Dim length As Integer

        If (val And &H80) <> 0 Then
            offset += 2
        Else
            offset += 1
        End If

        val = array(offset)

        If (val And &H80) <> 0 Then
            offset += 2
        Else
            offset += 1
        End If

        length = 0

        While array(offset + length) <> 0
            length += 1
        End While

        Return New Integer() {offset, length}
    End Function

    Private Shared Function GetUtf16(array As Byte(), offset As Integer) As Integer()
        Dim val As Integer = (array(offset + 1) And &HFF) << 8 Or (array(offset) And &HFF)

        If val = &H8000 Then
            Dim high As Integer = (array(offset + 3) And &HFF) << 8
            Dim low As Integer = (array(offset + 2) And &HFF)
            Return New Integer() {4, (high + low) * 2}
        End If

        Return New Integer() {2, val * 2}
    End Function

    Public Function Find(ByVal str As String) As Integer
        If str Is Nothing Then
            Return -1
        End If

        For i As Integer = 0 To m_stringOffsets.Length - 1
            Dim offset As Integer = m_stringOffsets(i)
            Dim length As Integer = GetShort(m_strings, offset)

            If length <> str.Length Then
                Continue For
            End If

            Dim j As Integer = 0
            For j = 0 To length - 1
                offset += 2
                If AscW(str.Chars(j)) <> GetShort(m_strings, offset) Then
                    Exit For
                End If
            Next

            If j = length Then
                Return i
            End If
        Next

        Return -1
    End Function

    Private Sub New()
    End Sub

    Private Function GetStyle(ByVal index As Integer) As Integer()
        If m_styleOffsets Is Nothing OrElse m_styles Is Nothing OrElse index >= m_styleOffsets.Length Then
            Return Nothing
        End If

        Dim offset As Integer = m_styleOffsets(index) \ 4
        Dim style As Integer()

        Dim count As Integer = 0
        For i As Integer = offset To m_styles.Length - 1
            If m_styles(i) = -1 Then
                Exit For
            End If
            count += 1
        Next

        If count = 0 OrElse (count Mod 3) <> 0 Then
            Return Nothing
        End If

        style = New Integer(count - 1) {}

        Dim j As Integer = 0
        For i As Integer = offset To m_styles.Length - 1
            If m_styles(i) = -1 Then
                Exit For
            End If
            style(j) = m_styles(i)
            j += 1
        Next

        Return style
    End Function
End Class
