using System.IO;
using System;
using QingYi.AXML.Android.Util;

namespace QingYi.AXML.Android.Content
{
    /**
     * @author Dmitry Skiba
     * 
     * Binary xml files parser.
     * 
     * Parser has only two states:
     * (1) Operational state, which parser obtains after first successful call
     * to next() and retains until open(), close(), or failed call to next().
     * (2) Closed state, which parser obtains after open(), close(), or failed
     * call to next(). In this state methods return invalid values or throw exceptions.
     * 
     * TODO:
     * 	* check all methods in closed state
     *
     */
    public class AXmlResourceParser
    {
        // data
        /*
         * All values are essentially indices, e.g. m_name is
         * an index of name in m_strings.
         */

        private IntReader m_reader;
        private bool m_operational = false;

        private StringBlock m_strings;
        private int[] m_resourceIDs;
        private readonly NamespaceStack m_namespaces = new NamespaceStack();

        private bool m_decreaseDepth;

        private int m_event;
        private int m_lineNumber;
        private int m_name;
        private int m_namespaceUri;
        private int[] m_attributes;
        private int m_idAttribute;
        private int m_classAttribute;
        private int m_styleAttribute;

        private static readonly string E_NOT_SUPPORTED = "Method is not supported.";

        private static readonly int ATTRIBUTE_IX_NAMESPACE_URI = 0;
        private static readonly int ATTRIBUTE_IX_NAME = 1;
        private static readonly int ATTRIBUTE_IX_VALUE_STRING = 2;
        private static readonly int ATTRIBUTE_IX_VALUE_TYPE = 3;
        private static readonly int ATTRIBUTE_IX_VALUE_DATA = 4;
        private static readonly int ATTRIBUTE_LENGTH = 5;

        private static readonly int CHUNK_AXML_FILE = 0x00080003;
        private static readonly int CHUNK_RESOURCEIDS = 0x00080180;
        private static readonly int CHUNK_XML_FIRST = 0x00100100;
        private static readonly int CHUNK_XML_START_NAMESPACE = 0x00100100;
        private static readonly int CHUNK_XML_END_NAMESPACE = 0x00100101;
        private static readonly int CHUNK_XML_START_TAG = 0x00100102;
        private static readonly int CHUNK_XML_END_TAG = 0x00100103;
        private static readonly int CHUNK_XML_TEXT = 0x00100104;
        private static readonly int CHUNK_XML_LAST = 0x00100104;

        // initialize
        public AXmlResourceParser()
        {
            ResetEventInfo();
        }

        public void Open(Stream stream)
        {
            Close();
            if (stream != null)
            {
                m_reader = new IntReader(stream, false);
            }
        }

        public void Close()
        {
            if (!m_operational)
            {
                return;
            }
            m_operational = false;
            m_reader.Close();
            m_reader = null;
            m_strings = null;
            m_resourceIDs = null;
            m_namespaces.Reset();
            ResetEventInfo();
        }

        ////////////////////////////////

        private void ResetEventInfo()
        {
            m_event = -1;
            m_lineNumber = -1;
            m_name = -1;
            m_namespaceUri = -1;
            m_attributes = null;
            m_idAttribute = -1;
            m_classAttribute = -1;
            m_styleAttribute = -1;
        }

        private void DoNext()
        {
            // Delayed initialization.
            if (m_strings == null)
            {
                ChunkUtil.ReadCheckType(m_reader, CHUNK_AXML_FILE);
                /*chunkSize*/
                m_reader.SkipInt();
                m_strings = StringBlock.Read(m_reader);
                m_namespaces.IncreaseDepth();
                m_operational = true;
            }

            if (m_event == END_DOCUMENT)
            {
                return;
            }

            int @event = m_event;
            ResetEventInfo();

            while (true)
            {
                if (m_decreaseDepth)
                {
                    m_decreaseDepth = false;
                    m_namespaces.DecreaseDepth();
                }

                // Fake END_DOCUMENT event.
                if (@event == END_TAG &&
                    m_namespaces.GetDepth() == 1 &&
                    m_namespaces.GetCurrentCount() == 0)
                {
                    m_event = END_DOCUMENT;
                    break;
                }

                int chunkType;
                if (@event == START_DOCUMENT)
                {
                    // Fake event, see CHUNK_XML_START_TAG handler.
                    chunkType = CHUNK_XML_START_TAG;
                }
                else
                {
                    chunkType = m_reader.ReadInt();
                }

                if (chunkType == CHUNK_RESOURCEIDS)
                {
                    int chunkSize = m_reader.ReadInt();
                    if (chunkSize < 8 || (chunkSize % 4) != 0)
                    {
                        throw new IOException($"Invalid resource ids size ({chunkSize}).");
                    }
                    m_resourceIDs = m_reader.ReadIntArray(chunkSize / 4 - 2);
                    continue;
                }

                if (chunkType < CHUNK_XML_FIRST || chunkType > CHUNK_XML_LAST)
                {
                    throw new IOException($"Invalid chunk type ({chunkType}).");
                }

                // Fake START_DOCUMENT event.
                if (chunkType == CHUNK_XML_START_TAG && @event == -1)
                {
                    m_event = START_DOCUMENT;
                    break;
                }

                // Common header.
                /*chunkSize*/
                m_reader.SkipInt();
                int lineNumber = m_reader.ReadInt();
                /*0xFFFFFFFF*/
                m_reader.SkipInt();

                if (chunkType == CHUNK_XML_START_NAMESPACE ||
                    chunkType == CHUNK_XML_END_NAMESPACE)
                {
                    if (chunkType == CHUNK_XML_START_NAMESPACE)
                    {
                        int prefix = m_reader.ReadInt();
                        int uri = m_reader.ReadInt();
                        m_namespaces.Push(prefix, uri);
                    }
                    else
                    {
                        /*prefix*/
                        m_reader.SkipInt();
                        /*uri*/
                        m_reader.SkipInt();
                        m_namespaces.Pop();
                    }
                    continue;
                }

                m_lineNumber = lineNumber;

                if (chunkType == CHUNK_XML_START_TAG)
                {
                    m_namespaceUri = m_reader.ReadInt();
                    m_name = m_reader.ReadInt();
                    /*flags?*/
                    m_reader.SkipInt();
                    int attributeCount = m_reader.ReadInt();
                    m_idAttribute = (attributeCount >> 16) - 1;
                    attributeCount &= 0xFFFF;
                    m_classAttribute = m_reader.ReadInt();
                    m_styleAttribute = (m_classAttribute >> 16) - 1;
                    m_classAttribute = (m_classAttribute & 0xFFFF) - 1;
                    m_attributes = m_reader.ReadIntArray(attributeCount * ATTRIBUTE_LENGHT);
                    for (int i = ATTRIBUTE_IX_VALUE_TYPE; i < m_attributes.Length;)
                    {
                        m_attributes[i] = (m_attributes[i] >> 24);
                        i += ATTRIBUTE_LENGHT;
                    }
                    m_namespaces.IncreaseDepth();
                    m_event = START_TAG;
                    break;
                }

                if (chunkType == CHUNK_XML_END_TAG)
                {
                    m_namespaceUri = m_reader.ReadInt();
                    m_name = m_reader.ReadInt();
                    m_event = END_TAG;
                    m_decreaseDepth = true;
                    break;
                }

                m_name = m_reader.ReadInt();
                /*?*/
                m_reader.SkipInt();
                /*?*/
                m_reader.SkipInt();
                m_event = TEXT;
                break;
            }
        }

        private int GetAttributeOffset(int index)
        {
            if (m_event != START_TAG)
            {
                throw new IndexOutOfRangeException("Current event is not START_TAG.");
            }
            int offset = index * 5;
            if (offset >= m_attributes.Length)
            {
                throw new IndexOutOfRangeException($"Invalid attribute index ({index}).");
            }
            return offset;
        }

        private int FindAttribute(string @namespace, string attribute)
        {
            if (m_strings == null || attribute == null)
            {
                return -1;
            }
            int name = m_strings.Find(attribute);
            if (name == -1)
            {
                return -1;
            }
            int uri = (@namespace != null) ?
                m_strings.Find(@namespace) :
                -1;
            for (int o = 0; o != m_attributes.Length; o += ATTRIBUTE_LENGHT)
            {
                if (name == m_attributes[o + ATTRIBUTE_IX_NAME] &&
                    (uri == -1 || uri == m_attributes[o + ATTRIBUTE_IX_NAMESPACE_URI]))
                {
                    return o / ATTRIBUTE_LENGHT;
                }
            }
            return -1;
        }

        public StringBlock GetStrings()
        {
            return m_strings;
        }

        // dummies
        public void SetInput(Stream stream, string inputEncoding)
        {
            throw new XmlPullParserException(E_NOT_SUPPORTED);
        }

        public void SetInput(TextReader reader)
        {
            throw new XmlPullParserException(E_NOT_SUPPORTED);
        }

        public string GetInputEncoding()
        {
            return null;
        }

        public int GetColumnNumber()
        {
            return -1;
        }

        public bool IsEmptyElementTag()
        {
            return false;
        }

        public bool IsWhitespace()
        {
            return false;
        }

        public void DefineEntityReplacementText(string entityName, string replacementText)
        {
            throw new XmlPullParserException(E_NOT_SUPPORTED);
        }

        public string GetNamespace(string prefix)
        {
            throw new InvalidOperationException(E_NOT_SUPPORTED);
        }

        public object GetProperty(string name)
        {
            return null;
        }

        public void SetProperty(string name, object value)
        {
            throw new XmlPullParserException(E_NOT_SUPPORTED);
        }

        public bool GetFeature(string feature)
        {
            return false;
        }

        public void SetFeature(string name, bool value)
        {
            throw new XmlPullParserException(E_NOT_SUPPORTED);
        }

        // attributes
        public string GetClassAttribute()
        {
            if (m_classAttribute == -1)
            {
                return null;
            }

            int offset = GetAttributeOffset(m_classAttribute);
            int value = m_attributes[offset + ATTRIBUTE_IX_VALUE_STRING];
            return m_strings.GetString(value);
        }

        public string GetIdAttribute()
        {
            if (m_idAttribute == -1)
            {
                return null;
            }

            int offset = GetAttributeOffset(m_idAttribute);
            int value = m_attributes[offset + ATTRIBUTE_IX_VALUE_STRING];
            return m_strings.GetString(value);
        }

        public int GetIdAttributeResourceValue(int defaultValue)
        {
            if (m_idAttribute == -1)
            {
                return defaultValue;
            }

            int offset = GetAttributeOffset(m_idAttribute);
            int valueType = m_attributes[offset + ATTRIBUTE_IX_VALUE_TYPE];
            if (valueType != TypedValue_TYPE_REFERENCE)
            {
                return defaultValue;
            }

            return m_attributes[offset + ATTRIBUTE_IX_VALUE_DATA];
        }

        public int GetStyleAttribute()
        {
            if (m_styleAttribute == -1)
            {
                return 0;
            }
            int offset = GetAttributeOffset(m_styleAttribute);
            return m_attributes[offset + ATTRIBUTE_IX_VALUE_DATA];
        }

        public int GetAttributeCount()
        {
            if (m_event != START_TAG)
            {
                return -1;
            }
            return m_attributes.Length / ATTRIBUTE_LENGHT;
        }

        public string GetAttributeNamespace(int index)
        {
            int offset = GetAttributeOffset(index);
            int @namespace = m_attributes[offset + ATTRIBUTE_IX_NAMESPACE_URI];
            if (@namespace == -1)
            {
                return "";
            }
            return m_strings.GetString(@namespace);
        }

        public string GetAttributePrefix(int index)
        {
            int offset = GetAttributeOffset(index);
            int uri = m_attributes[offset + ATTRIBUTE_IX_NAMESPACE_URI];
            int prefix = m_namespaces.FindPrefix(uri);
            if (prefix == -1)
            {
                return "";
            }
            return m_strings.GetString(prefix);
        }

        public string GetAttributeName(int index)
        {
            int offset = GetAttributeOffset(index);
            int name = m_attributes[offset + ATTRIBUTE_IX_NAME];
            if (name == -1)
            {
                return "";
            }
            return m_strings.GetString(name);
        }

        public int GetAttributeNameResource(int index)
        {
            int offset = GetAttributeOffset(index);
            int name = m_attributes[offset + ATTRIBUTE_IX_NAME];
            if (m_resourceIDs == null ||
                name < 0 || name >= m_resourceIDs.Length)
            {
                return 0;
            }
            return m_resourceIDs[name];
        }

        public int GetAttributeValueType(int index)
        {
            int offset = GetAttributeOffset(index);
            return m_attributes[offset + ATTRIBUTE_IX_VALUE_TYPE];
        }

        public int GetAttributeValueData(int index)
        {
            int offset = GetAttributeOffset(index);
            return m_attributes[offset + ATTRIBUTE_IX_VALUE_DATA];
        }

        public string GetAttributeValue(int index)
        {
            int offset = GetAttributeOffset(index);
            int valueType = m_attributes[offset + ATTRIBUTE_IX_VALUE_TYPE];
            if (valueType == TypedValue.TYPE_STRING)
            {
                int valueString = m_attributes[offset + ATTRIBUTE_IX_VALUE_STRING];
                return m_strings.GetString(valueString);
            }
            return ""; // TypedValue.coerceToString(valueType, valueData);
        }

        public bool GetAttributeBooleanValue(int index, bool defaultValue)
        {
            return GetAttributeIntValue(index, defaultValue ? 1 : 0) != 0;
        }

        public float GetAttributeFloatValue(int index, float defaultValue)
        {
            int offset = GetAttributeOffset(index);
            int valueType = m_attributes[offset + ATTRIBUTE_IX_VALUE_TYPE];
            if (valueType == TypedValue.TYPE_FLOAT)
            {
                int valueData = m_attributes[offset + ATTRIBUTE_IX_VALUE_DATA];
                return BitConverter.ToSingle(BitConverter.GetBytes(valueData), 0);
            }
            return defaultValue;
        }

        public int GetAttributeIntValue(int index, int defaultValue)
        {
            int offset = GetAttributeOffset(index);
            int valueType = m_attributes[offset + ATTRIBUTE_IX_VALUE_TYPE];
            if (valueType >= TypedValue.TYPE_FIRST_INT &&
                valueType <= TypedValue.TYPE_LAST_INT)
            {
                return m_attributes[offset + ATTRIBUTE_IX_VALUE_DATA];
            }
            return defaultValue;
        }

        public int GetAttributeUnsignedIntValue(int index, int defaultValue)
        {
            return GetAttributeIntValue(index, defaultValue);
        }

        public int GetAttributeResourceValue(int index, int defaultValue)
        {
            int offset = GetAttributeOffset(index);
            int valueType = m_attributes[offset + ATTRIBUTE_IX_VALUE_TYPE];
            if (valueType == TypedValue.TYPE_REFERENCE)
            {
                return m_attributes[offset + ATTRIBUTE_IX_VALUE_DATA];
            }
            return defaultValue;
        }

        public string GetAttributeValue(string @namespace, string attribute)
        {
            int index = FindAttribute(@namespace, attribute);
            if (index == -1)
            {
                return null;
            }
            return GetAttributeValue(index);
        }

        public bool GetAttributeBooleanValue(string @namespace, string attribute, bool defaultValue)
        {
            int index = FindAttribute(@namespace, attribute);
            if (index == -1)
            {
                return defaultValue;
            }
            return GetAttributeBooleanValue(index, defaultValue);
        }

        public float GetAttributeFloatValue(string @namespace, string attribute, float defaultValue)
        {
            int index = FindAttribute(@namespace, attribute);
            if (index == -1)
            {
                return defaultValue;
            }
            return GetAttributeFloatValue(index, defaultValue);
        }

        public int GetAttributeIntValue(string @namespace, string attribute, int defaultValue)
        {
            int index = FindAttribute(@namespace, attribute);
            if (index == -1)
            {
                return defaultValue;
            }
            return GetAttributeIntValue(index, defaultValue);
        }

        public int GetAttributeUnsignedIntValue(string @namespace, string attribute, int defaultValue)
        {
            int index = FindAttribute(@namespace, attribute);
            if (index == -1)
            {
                return defaultValue;
            }
            return GetAttributeUnsignedIntValue(index, defaultValue);
        }

        public int GetAttributeResourceValue(string @namespace, string attribute, int defaultValue)
        {
            int index = FindAttribute(@namespace, attribute);
            if (index == -1)
            {
                return defaultValue;
            }
            return GetAttributeResourceValue(index, defaultValue);
        }

        public int GetAttributeListValue(int index, string[] options, int defaultValue)
        {
            // TODO implement
            return 0; // Placeholder implementation
        }

        public int GetAttributeListValue(string @namespace, string attribute, string[] options, int defaultValue)
        {
            // TODO implement
            return 0; // Placeholder implementation
        }

        public string GetAttributeType(int index)
        {
            return "CDATA";
        }

        public bool IsAttributeDefault(int index)
        {
            return false;
        }

        // iteration
        public int Next()
        {
            if (m_reader == null)
            {
                throw new XmlPullParserException("Parser is not opened.", this, null);
            }
            try
            {
                DoNext();
                return m_event;
            }
            catch (IOException e)
            {
                Close();
                throw e;
            }
        }

        public int NextToken()
        {
            return Next();
        }

        public int NextTag()
        {
            int eventType = Next();
            if (eventType == TEXT && IsWhitespace())
            {
                eventType = Next();
            }
            if (eventType != START_TAG && eventType != END_TAG)
            {
                throw new XmlPullParserException("Expected start or end tag.", this, null);
            }
            return eventType;
        }

        public string NextText()
        {
            if (GetEventType() != START_TAG)
            {
                throw new XmlPullParserException("Parser must be on START_TAG to read next text.", this, null);
            }
            int eventType = Next();
            if (eventType == TEXT)
            {
                string result = GetText();
                eventType = Next();
                if (eventType != END_TAG)
                {
                    throw new XmlPullParserException("Event TEXT must be immediately followed by END_TAG.", this, null);
                }
                return result;
            }
            else if (eventType == END_TAG)
            {
                return "";
            }
            else
            {
                throw new XmlPullParserException("Parser must be on START_TAG or TEXT to read text.", this, null);
            }
        }

        public void Require(int type, string @namespace, string name)
        {
            if (type != GetEventType() ||
                (@namespace != null && @namespace != GetNamespace()) ||
                (name != null && name != GetName()))
            {
                throw new XmlPullParserException(TYPES[type] + " is expected.", this, null);
            }
        }

        public int GetDepth()
        {
            return m_namespaces.GetDepth() - 1;
        }

        public int GetEventType()
        {
            return m_event;
        }

        public int GetLineNumber()
        {
            return m_lineNumber;
        }

        public string GetName()
        {
            if (m_name == -1 || (m_event != START_TAG && m_event != END_TAG))
            {
                return null;
            }
            return m_strings.GetString(m_name);
        }

        public string GetText()
        {
            if (m_name == -1 || m_event != TEXT)
            {
                return null;
            }
            return m_strings.GetString(m_name);
        }

        public char[] GetTextCharacters(int[] holderForStartAndLength)
        {
            string text = GetText();
            if (text == null)
            {
                return null;
            }
            holderForStartAndLength[0] = 0;
            holderForStartAndLength[1] = text.Length;
            char[] chars = new char[text.Length];
            text.CopyTo(0, chars, 0, text.Length);
            return chars;
        }

        public string GetNamespace()
        {
            return m_strings.GetString(m_namespaceUri);
        }

        public string GetPrefix()
        {
            int prefix = m_namespaces.FindPrefix(m_namespaceUri);
            return m_strings.GetString(prefix);
        }

        public string GetPositionDescription()
        {
            return "XML line #" + GetLineNumber();
        }

        public int GetNamespaceCount(int depth)
        {
            return m_namespaces.GetAccumulatedCount(depth);
        }

        public string GetNamespacePrefix(int pos)
        {
            int prefix = m_namespaces.GetPrefix(pos);
            return m_strings.GetString(prefix);
        }

        public string GetNamespaceUri(int pos)
        {
            int uri = m_namespaces.GetUri(pos);
            return m_strings.GetString(uri);
        }
    }
}
