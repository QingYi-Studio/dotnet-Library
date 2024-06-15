using System.IO;

namespace QingYi.AXML.Android.XmlPull.V1
{
    public interface IXmlPullParser
    {
        string NO_NAMESPACE { get; }

        int START_DOCUMENT { get; }
        int END_DOCUMENT { get; }
        int START_TAG { get; }
        int END_TAG { get; }
        int TEXT { get; }
        int CDSECT { get; }
        int ENTITY_REF { get; }
        int IGNORABLE_WHITESPACE { get; }
        int PROCESSING_INSTRUCTION { get; }
        int COMMENT { get; }
        int DOCDECL { get; }

        string[] TYPES { get; }

        string FEATURE_PROCESS_NAMESPACES { get; }
        string FEATURE_REPORT_NAMESPACE_ATTRIBUTES { get; }
        string FEATURE_PROCESS_DOCDECL { get; }
        string FEATURE_VALIDATION { get; }

        void SetFeature(string name, bool state);

        bool GetFeature(string name);

        void SetProperty(string name, object value);

        object GetProperty(string name);

        void SetInput(TextReader input);

        void SetInput(Stream inputStream, string inputEncoding);

        string GetInputEncoding();

        void DefineEntityReplacementText(string entityName, string replacementText);

        int GetNamespaceCount(int depth);

        string GetNamespaceUri(int pos);

        string GetNamespace(string prefix);

        string GetPositionDescription();

        int GetColumnNumber();

        bool IsWhitespace();

        string GetText();

        char[] GetTextCharacters(out int start);

        string GetNamespace();

        string GetName();

        string GetPrefix();

        bool IsEmptyElementTag();

        int GetAttributeCount();

        string GetAttributeNamespace(int index);

        string GetAttributeName(int index);

        string GetAttributePrefix(int index);

        string GetAttributeType(int index);

        bool IsAttributeDefault(int index);

        string GetAttributeValue(int index);

        string GetAttributeValue(string namespaceUri, string name);

        int GetEventType();

        int Next();

        int NextToken();

        void Require(int type, string namespaceUri, string name);

        string NextText();

        int NextTag();
    }
}