using System.IO;
using System;

namespace QingYi.AXML.Android.content
{
    /**
     *
     * Simple helper class that allows reading of integers.
     * 
     * TODO:
     * 	* implement buffering
     *
     */
    public sealed class IntReader
    {
        private Stream m_stream;
        private bool m_bigEndian;
        private int m_position;

        public IntReader(Stream stream, bool bigEndian)
        {
            Reset(stream, bigEndian);
        }

        public void Reset(Stream stream, bool bigEndian)
        {
            m_stream = stream;
            m_bigEndian = bigEndian;
            m_position = 0;
        }

        public void Close()
        {
            if (m_stream == null)
            {
                return;
            }
            try
            {
                m_stream.Close();
            }
            catch (IOException)
            {
                // Ignore IOException on close
            }
            Reset(null, false);
        }

        public Stream GetStream()
        {
            return m_stream;
        }

        public bool IsBigEndian()
        {
            return m_bigEndian;
        }

        public void SetBigEndian(bool bigEndian)
        {
            m_bigEndian = bigEndian;
        }

        public int ReadByte()
        {
            return ReadInt(1);
        }

        public int ReadShort()
        {
            return ReadInt(2);
        }

        public int ReadInt()
        {
            return ReadInt(4);
        }

        public void ReadFully(byte[] b)
        {
            new BinaryReader(m_stream).ReadBytes(b.Length);
        }

        public int ReadInt(int length)
        {
            if (length < 0 || length > 4)
            {
                throw new ArgumentException();
            }

            int result = 0;
            if (m_bigEndian)
            {
                for (int i = (length - 1) * 8; i >= 0; i -= 8)
                {
                    int b = m_stream.ReadByte();
                    if (b == -1)
                    {
                        throw new EndOfStreamException();
                    }
                    m_position += 1;
                    result |= (b << i);
                }
            }
            else
            {
                length *= 8;
                for (int i = 0; i != length; i += 8)
                {
                    int b = m_stream.ReadByte();
                    if (b == -1)
                    {
                        throw new EndOfStreamException();
                    }
                    m_position += 1;
                    result |= (b << i);
                }
            }
            return result;
        }

        public int[] ReadIntArray(int length)
        {
            int[] array = new int[length];
            ReadIntArray(array, 0, length);
            return array;
        }

        public void ReadIntArray(int[] array, int offset, int length)
        {
            for (; length > 0; length -= 1)
            {
                array[offset++] = ReadInt();
            }
        }

        public byte[] ReadByteArray(int length)
        {
            byte[] array = new byte[length];
            int read = m_stream.Read(array, 0, length);
            m_position += read;
            if (read != length)
            {
                throw new EndOfStreamException();
            }
            return array;
        }

        public void Skip(int bytes)
        {
            if (bytes <= 0)
            {
                return;
            }
            long skipped = m_stream.Seek(bytes, SeekOrigin.Current);
            m_position += (int)skipped;
            if (skipped != bytes)
            {
                throw new EndOfStreamException();
            }
        }

        public void SkipInt()
        {
            Skip(4);
        }

        public int Available()
        {
            return (int)(m_stream.Length - m_stream.Position);
        }

        public int GetPosition()
        {
            return m_position;
        }
    }
}
