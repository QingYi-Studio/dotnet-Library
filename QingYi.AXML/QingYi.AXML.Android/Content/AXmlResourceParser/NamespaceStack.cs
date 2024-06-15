using System;

namespace QingYi.AXML.Android.Content
{
    internal class NamespaceStack
    {
        private int[] m_data;
        private int m_dataLength;
        private int m_count;
        private int m_depth;

        public NamespaceStack()
        {
            m_data = new int[32];
        }

        public void Reset()
        {
            m_dataLength = 0;
            m_count = 0;
            m_depth = 0;
        }

        public int GetTotalCount()
        {
            return m_count;
        }

        public int GetCurrentCount()
        {
            if (m_dataLength == 0)
            {
                return 0;
            }
            int offset = m_dataLength - 1;
            return m_data[offset];
        }

        public int GetAccumulatedCount(int depth)
        {
            if (m_dataLength == 0 || depth < 0)
            {
                return 0;
            }
            if (depth > m_depth)
            {
                depth = m_depth;
            }
            int accumulatedCount = 0;
            int offset = 0;
            for (; depth != 0; --depth)
            {
                int count = m_data[offset];
                accumulatedCount += count;
                offset += (2 + count * 2);
            }
            return accumulatedCount;
        }

        public void Push(int prefix, int uri)
        {
            if (m_depth == 0)
            {
                IncreaseDepth();
            }
            EnsureDataCapacity();
            int offset = m_dataLength - 1;
            int count = m_data[offset];
            m_data[offset - 1 - count * 2] = count + 1;
            m_data[offset] = prefix;
            m_data[offset + 1] = uri;
            m_data[offset + 2] = count + 1;
            m_dataLength += 2;
            m_count += 1;
        }

        public bool Pop(int prefix, int uri)
        {
            if (m_dataLength == 0)
            {
                return false;
            }
            int offset = m_dataLength - 1;
            int count = m_data[offset];
            for (int i = 0, o = offset - 2; i != count; ++i, o -= 2)
            {
                if (m_data[o] != prefix || m_data[o + 1] != uri)
                {
                    continue;
                }
                count -= 1;
                if (i == 0)
                {
                    m_data[o] = count;
                    o -= (1 + count * 2);
                    m_data[o] = count;
                }
                else
                {
                    m_data[offset] = count;
                    offset -= (1 + 2 + count * 2);
                    m_data[offset] = count;
                    Array.Copy(m_data, o + 2, m_data, o, m_dataLength - o);
                }
                m_dataLength -= 2;
                m_count -= 1;
                return true;
            }
            return false;
        }

        public bool Pop()
        {
            if (m_dataLength == 0)
            {
                return false;
            }
            int offset = m_dataLength - 1;
            int count = m_data[offset];
            if (count == 0)
            {
                return false;
            }
            count -= 1;
            offset -= 2;
            m_data[offset] = count;
            offset -= (1 + count * 2);
            m_data[offset] = count;
            m_dataLength -= 2;
            m_count -= 1;
            return true;
        }

        private int Find(int prefixOrUri, bool prefix)
        {
            if (m_dataLength == 0)
            {
                return -1;
            }
            int offset = m_dataLength - 1;
            for (int i = m_depth; i != 0; --i)
            {
                int count = m_data[offset];
                offset -= 2;
                for (; count != 0; --count)
                {
                    if (prefix)
                    {
                        if (m_data[offset] == prefixOrUri)
                        {
                            return m_data[offset + 1];
                        }
                    }
                    else
                    {
                        if (m_data[offset + 1] == prefixOrUri)
                        {
                            return m_data[offset];
                        }
                    }
                    offset -= 2;
                }
            }
            return -1;
        }

        private int Get(int index, bool prefix)
        {
            if (m_dataLength == 0 || index < 0)
            {
                return -1;
            }
            int offset = 0;
            for (int i = m_depth; i != 0; --i)
            {
                int count = m_data[offset];
                if (index >= count)
                {
                    index -= count;
                    offset += (2 + count * 2);
                    continue;
                }
                offset += (1 + index * 2);
                if (!prefix)
                {
                    offset += 1;
                }
                return m_data[offset];
            }
            return -1;
        }

        public int GetPrefix(int index)
        {
            return Get(index, true);
        }

        public int GetUri(int index)
        {
            return Get(index, false);
        }

        public int FindPrefix(int uri)
        {
            return Find(uri, false);
        }

        public int FindUri(int prefix)
        {
            return Find(prefix, true);
        }

        public int GetDepth()
        {
            return m_depth;
        }

        public void IncreaseDepth()
        {
            EnsureDataCapacity();
            int offset = m_dataLength;
            m_data[offset] = 0;
            m_data[offset + 1] = 0;
            m_dataLength += 2;
            m_depth += 1;
        }

        public void DecreaseDepth()
        {
            if (m_dataLength == 0)
            {
                return;
            }
            int offset = m_dataLength - 1;
            int count = m_data[offset];
            if ((offset - 1 - count * 2) == 0)
            {
                return;
            }
            m_dataLength -= 2 + count * 2;
            m_count -= count;
            m_depth -= 1;
        }

        private void EnsureDataCapacity()
        {
            int available = (m_data.Length - m_dataLength);
            if (available > 2)
            {
                return;
            }
            int newLength = (m_data.Length + available) * 2;
            int[] newData = new int[newLength];
            Array.Copy(m_data, 0, newData, 0, m_dataLength);
            m_data = newData;
        }
    }
}
