using QingYi.AXML.Android.content;

namespace QingYi.AXML.Android.Content
{
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
    }
}
