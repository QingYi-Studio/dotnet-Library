using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace QingYi.AXML.ResourceParser
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
    public class Parser
    {
        private IntReader.IntReader m_reader;
        private bool m_operational = false;
        private StringBlock.StringBlock m_strings;
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

        private static readonly int
            ATTRIBUTE_IX_NAMESPACE_URI = 0,
            ATTRIBUTE_IX_NAME = 1,
            ATTRIBUTE_IX_VALUE_STRING = 2,
            ATTRIBUTE_IX_VALUE_TYPE = 3,
            ATTRIBUTE_IX_VALUE_DATA = 4,
            ATTRIBUTE_LENGTH = 5;

        private static readonly int
            CHUNK_AXML_FILE = 0x00080003,
            CHUNK_RESOURCEIDS = 0x00080180,
            CHUNK_XML_FIRST = 0x00100100,
            CHUNK_XML_START_NAMESPACE = 0x00100100,
            CHUNK_XML_END_NAMESPACE = 0x00100101,
            CHUNK_XML_START_TAG = 0x00100102,
            CHUNK_XML_END_TAG = 0x00100103,
            CHUNK_XML_TEXT = 0x00100104,
            CHUNK_XML_LAST = 0x00100104;

        public Parser()
        {
            ResetEventInfo();
        }

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

        public void Open(Stream stream)
        {
            Close();
            if (stream != null)
            {
                m_reader = new IntReader.IntReader(stream, false);
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
            // Assuming m_namespaces is a resettable object
            m_namespaces.Reset(); // Resetting m_namespaces if it implements IResettable
            ResetEventInfo();
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
                close();
                throw e;
            }
        }

        public int NextToken()
        {
            return Next();
        }

        public int nextTag()
        {
            int eventType = Next();
            if (eventType == TEXT && isWhitespace())
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
            if (getEventType() != START_TAG)
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

        public void Require(int type, string _namespace, string name)
        {
            if (type != GetEventType() ||
                (_namespace != null && _namespace != GetNamespace()) ||
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
            return m_strings.getString(m_name);
        }

        public string getText()
        {
            if (m_name == -1 || m_event != TEXT)
            {
                return null;
            }
            return m_strings.getString(m_name);
        }

        public char[] getTextCharacters(int[] holderForStartAndLength)
        {
            string text = getText();
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

        public string getNamespace()
        {
            return m_strings.getString(m_namespaceUri);
        }

        public string getPrefix()
        {
            int prefix = m_namespaces.findPrefix(m_namespaceUri);
            return m_strings.getString(prefix);
        }

        public string getPositionDescription()
        {
            return "XML line #" + getLineNumber();
        }

        public int getNamespaceCount(int depth)
        {
            return m_namespaces.getAccumulatedCount(depth);
        }

        public string getNamespacePrefix(int pos)
        {
            int prefix = m_namespaces.getPrefix(pos);
            return m_strings.getString(prefix);
        }

        public string getNamespaceUri(int pos)
        {
            int uri = m_namespaces.getUri(pos);
            return m_strings.getString(uri);
        }

        private void close()
        {
            if (!m_operational)
            {
                return;
            }
            m_operational = false;
            m_reader.close();
            m_reader = null;
            m_strings = null;
            m_namespaceUri = 0; // Reset namespace URI
            m_namespaces.reset();
            ResetEventInfo();
        }

        private void doNext()
        {
            // Implementation of doNext() method
            // This should include parsing logic to advance through XML events
            // m_event should be updated accordingly
        }

        private bool isWhitespace()
        {
            // Implementation of isWhitespace() method
            // Check if current text content is whitespace
            return false; // Placeholder, implement as needed
        }
    }
}
