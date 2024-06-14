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
        // data

        /*
         * All values are essentially indices, e.g. m_name is
         * an index of name in m_strings.
         */
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

        // attributes

        // dummies
    }
}
