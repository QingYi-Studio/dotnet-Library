using System;
using System.IO;
using System.Text;

namespace QingYi.AXML.Android.content
{
    public class StringBlock
    {
        private int[] m_stringOffsets;
        private byte[] m_strings;
        private int[] m_styleOffsets;
        private int[] m_styles;
        private bool m_isUTF8;
        private const int CHUNK_TYPE = 0x001C0001;
        private const int UTF8_FLAG = 0x00000100;

        private readonly Decoder UTF8_DECODER = Encoding.UTF8.GetDecoder();
        private readonly Decoder UTF16LE_DECODER = Encoding.Unicode.GetDecoder();

        /**
        * Reads whole (including chunk type) string block from stream.
        * Stream must be at the chunk type.
        */
        public static StringBlock Read(IntReader reader)
        {
            ChunkUtil.ReadCheckType(reader, CHUNK_TYPE);
            int chunkSize = reader.ReadInt();
            int stringCount = reader.ReadInt();
            int styleOffsetCount = reader.ReadInt();
            int flags = reader.ReadInt();
            int stringsOffset = reader.ReadInt();
            int stylesOffset = reader.ReadInt();

            StringBlock block = new StringBlock
            {
                m_isUTF8 = (flags & UTF8_FLAG) != 0,
                m_stringOffsets = reader.ReadIntArray(stringCount)
            };
            if (styleOffsetCount != 0)
            {
                block.m_styleOffsets = reader.ReadIntArray(styleOffsetCount);
            }

            {
                int size = (stylesOffset == 0) ? chunkSize - stringsOffset : stylesOffset - stringsOffset;
                block.m_strings = new byte[size];
                reader.ReadFully(block.m_strings);
            }

            if (stylesOffset != 0)
            {
                int size = chunkSize - stylesOffset;
                if (size % 4 != 0)
                {
                    throw new IOException($"Style data size is not multiple of 4 ({size}).");
                }
                block.m_styles = reader.ReadIntArray(size / 4);
            }

            return block;
        }

        /**
         * Returns number of strings in block.
         */
        public int GetCount()
        {
            return m_stringOffsets != null ? m_stringOffsets.Length : 0;
        }

        public string GetString(int index)
        {
            if (index < 0 || m_stringOffsets == null || index >= m_stringOffsets.Length)
            {
                return null;
            }
            int offset = m_stringOffsets[index];
            int length;
            int[] val;
            if (m_isUTF8)
            {
                val = GetUtf8(m_strings, offset);
                offset = val[0];
            }
            else
            {
                val = GetUtf16(m_strings, offset);
                offset += val[0];
            }
            length = val[1];
            return DecodeString(offset, length);
        }

        private string DecodeString(int offset, int length)
        {
            try
            {
                byte[] bytes = new byte[length];
                Array.Copy(m_strings, offset, bytes, 0, length);

                string decodedString;
                if (m_isUTF8)
                {
                    decodedString = Encoding.UTF8.GetString(bytes);
                }
                else
                {
                    decodedString = Encoding.Unicode.GetString(bytes);
                }

                return decodedString;
            }
            catch (DecoderFallbackException)
            {
                return null;
            }
        }

        private static int GetShort(byte[] array, int offset)
        {
            return (array[offset + 1] & 0xff) << 8 | (array[offset] & 0xff);
        }

        private static int[] GetUtf8(byte[] array, int offset)
        {
            int val = array[offset];
            int length;

            if ((val & 0x80) != 0)
            {
                offset += 2;
            }
            else
            {
                offset += 1;
            }

            val = array[offset];

            if ((val & 0x80) != 0)
            {
                offset += 2;
            }
            else
            {
                offset += 1;
            }

            length = 0;
            while (array[offset + length] != 0)
            {
                length++;
            }

            return new int[] { offset, length };
        }

        private static int[] GetUtf16(byte[] array, int offset)
        {
            int val = (array[offset + 1] & 0xff) << 8 | (array[offset] & 0xff);

            if (val == 0x8000)
            {
                int high = (array[offset + 3] & 0xFF) << 8;
                int low = (array[offset + 2] & 0xFF);
                return new int[] { 4, (high + low) * 2 };
            }
            else
            {
                return new int[] { 2, val * 2 };
            }
        }

        /**
         * Not yet implemented.
         * <p>
         * Returns string with style information (if any).
         */
        public string Get(int index)
        {
            return GetString(index);
        }

        /**
         * Returns string with style tags (html-like). 
         */
        public string GetHTML(int index)
        {
            string raw = GetString(index);
            if (raw == null)
            {
                return null;
            }

            int[] style = GetStyle(index);
            if (style == null)
            {
                return raw;
            }

            StringBuilder html = new StringBuilder(raw.Length + 32);
            int offset = 0;

            while (true)
            {
                int i = -1;
                for (int j = 0; j < style.Length; j += 3)
                {
                    if (style[j + 1] == -1)
                    {
                        continue;
                    }
                    if (i == -1 || style[i + 1] > style[j + 1])
                    {
                        i = j;
                    }
                }
                int start = (i != -1) ? style[i + 1] : raw.Length;

                for (int j = 0; j < style.Length; j += 3)
                {
                    int end = style[j + 2];
                    if (end == -1 || end >= start)
                    {
                        continue;
                    }
                    if (offset <= end)
                    {
                        html.Append(raw.Substring(offset, end - offset + 1));
                        offset = end + 1;
                    }
                    style[j + 2] = -1;
                    html.Append('<');
                    html.Append('/');
                    html.Append(GetString(style[j]));
                    html.Append('>');
                }

                if (offset < start)
                {
                    html.Append(raw.Substring(offset, start - offset));
                    offset = start;
                }

                if (i == -1)
                {
                    break;
                }

                html.Append('<');
                html.Append(GetString(style[i]));
                html.Append('>');
                style[i + 1] = -1;
            }

            return html.ToString();
        }

        /**
        * Finds index of the string.
        * Returns -1 if the string was not found.
        */
        public int Find(string str)
        {
            if (str == null)
            {
                return -1;
            }

            for (int i = 0; i != m_stringOffsets.Length; ++i)
            {
                int offset = m_stringOffsets[i];
                int length = GetShort(m_strings, offset);

                if (length != str.Length)
                {
                    continue;
                }

                int j = 0;
                for (; j != length; ++j)
                {
                    offset += 2;
                    if (str[j] != GetShort(m_strings, offset))
                    {
                        break;
                    }
                }

                if (j == length)
                {
                    return i;
                }
            }

            return -1;
        }

        // implementation

        private StringBlock() { }

        /**
        * Returns style information - array of int triplets,
        * where in each triplet:
        * first int is index of tag name ('b','i', etc.)
        * second int is tag start index in string
        * third int is tag end index in string
        */
        private int[] GetStyle(int index)
        {
            if (m_styleOffsets == null || m_styles == null ||
                index >= m_styleOffsets.Length)
            {
                return null;
            }

            int offset = m_styleOffsets[index] / 4;
            int[] style;
            {
                int count = 0;
                for (int i = offset; i < m_styles.Length; ++i)
                {
                    if (m_styles[i] == -1)
                    {
                        break;
                    }
                    count += 1;
                }
                if (count == 0 || (count % 3) != 0)
                {
                    return null;
                }
                style = new int[count];
            }

            for (int i = offset, j = 0; i < m_styles.Length;)
            {
                if (m_styles[i] == -1)
                {
                    break;
                }
                style[j++] = m_styles[i++];
            }

            return style;
        }

        private static int GetShort(int[] array, int offset)
        {
            int value = array[offset / 4];
            if ((offset % 4) / 2 == 0)
            {
                return (value & 0xFFFF);
            }
            else
            {
                return (value >> 16);
            }
        }
    }
}
