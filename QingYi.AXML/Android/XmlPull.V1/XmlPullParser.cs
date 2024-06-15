using QingYi.AXML.Android.Content;
using QingYi.AXML.Exception.V1;
using System.IO;
using System.Runtime.CompilerServices;

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

        string GetNamespacePrefix(int pos);

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

    public class XmlPullParser : IXmlPullParser
    {
        private readonly string E_NOT_SUPPORTED = "Method is not supported.";
        private readonly NamespaceStack m_namespaces = new NamespaceStack();
        private StringBlock m_strings;

        /** This constant represents the default namespace (empty string "") */
        public string NO_NAMESPACE { get; } = "";

        // ----------------------------------------------------------------------------
        // EVENT TYPES as reported by next()

        /**
         * Signalize that parser is at the very beginning of the document
         * and nothing was read yet.
         * This event type can only be observed by calling getEvent()
         * before the first call to next(), nextToken, or nextTag()</a>).
         *
         * @see #next
         * @see #nextToken
         */
        public int START_DOCUMENT { get; } = 0;

        /**
         * Logical end of the xml document. Returned from getEventType, next()
         * and nextToken()
         * when the end of the input document has been reached.
         * <p><strong>NOTE:</strong> calling again
         * <a href="#next()">next()</a> or <a href="#nextToken()">nextToken()</a>
         * will result in exception being thrown.
         *
         * @see #next
         * @see #nextToken
         */
        public int END_DOCUMENT { get; } = 1;

        /**
         * Returned from getEventType(),
         * <a href="#next()">next()</a>, <a href="#nextToken()">nextToken()</a> when
         * a start tag was read.
         * The name of start tag is available from getName(), its namespace and prefix are
         * available from getNamespace() and getPrefix()
         * if <a href='#FEATURE_PROCESS_NAMESPACES'>namespaces are enabled</a>.
         * See getAttribute* methods to retrieve element attributes.
         * See getNamespace* methods to retrieve newly declared namespaces.
         *
         * @see #next
         * @see #nextToken
         * @see #getName
         * @see #getPrefix
         * @see #getNamespace
         * @see #getAttributeCount
         * @see #getDepth
         * @see #getNamespaceCount
         * @see #getNamespace
         * @see #FEATURE_PROCESS_NAMESPACES
         */
        public int START_TAG { get; } = 2;

        /**
         * Returned from getEventType(), <a href="#next()">next()</a>, or
         * <a href="#nextToken()">nextToken()</a> when an end tag was read.
         * The name of start tag is available from getName(), its
         * namespace and prefix are
         * available from getNamespace() and getPrefix().
         *
         * @see #next
         * @see #nextToken
         * @see #getName
         * @see #getPrefix
         * @see #getNamespace
         * @see #FEATURE_PROCESS_NAMESPACES
         */
        public int END_TAG { get; } = 3;

        /**
         * Character data was read and will is available by calling getText().
         * <p><strong>Please note:</strong> <a href="#next()">next()</a> will
         * accumulate multiple
         * events into one TEXT event, skipping IGNORABLE_WHITESPACE,
         * PROCESSING_INSTRUCTION and COMMENT events,
         * In contrast, <a href="#nextToken()">nextToken()</a> will stop reading
         * text when any other event is observed.
         * Also, when the state was reached by calling next(), the text value will
         * be normalized, whereas getText() will
         * return unnormalized content in the case of nextToken(). This allows
         * an exact roundtrip without chnanging line ends when examining low
         * level events, whereas for high level applications the text is
         * normalized apropriately.
         *
         * @see #next
         * @see #nextToken
         * @see #getText
         */
        public int TEXT { get; } = 4;

        // ----------------------------------------------------------------------------
        // additional events exposed by lower level nextToken()

        /**
         * A CDATA sections was just read;
         * this token is available only from calls to <a href="#nextToken()">nextToken()</a>.
         * A call to next() will accumulate various text events into a single event
         * of type TEXT. The text contained in the CDATA section is available
         * by callling getText().
         *
         * @see #nextToken
         * @see #getText
         */
        public int CDSECT { get; } = 5;

        /**
         * An entity reference was just read;
         * this token is available from <a href="#nextToken()">nextToken()</a>
         * only. The entity name is available by calling getName(). If available,
         * the replacement text can be obtained by calling getTextt(); otherwise,
         * the user is responsibile for resolving the entity reference.
         * This event type is never returned from next(); next() will
         * accumulate the replacement text and other text
         * events to a single TEXT event.
         *
         * @see #nextToken
         * @see #getText
         */
        public int ENTITY_REF { get; } = 6;

        /**
         * Ignorable whitespace was just read.
         * This token is available only from <a href="#nextToken()">nextToken()</a>).
         * For non-validating
         * parsers, this event is only reported by nextToken() when outside
         * the root element.
         * Validating parsers may be able to detect ignorable whitespace at
         * other locations.
         * The ignorable whitespace string is available by calling getText()
         *
         * <p><strong>NOTE:</strong> this is different from calling the
         *  isWhitespace() method, since text content
         *  may be whitespace but not ignorable.
         *
         * Ignorable whitespace is skipped by next() automatically; this event
         * type is never returned from next().
         *
         * @see #nextToken
         * @see #getText
         */
        public int IGNORABLE_WHITESPACE { get; } = 7;

        /**
         * An XML processing instruction declaration was just read. This
         * event type is available only via <a href="#nextToken()">nextToken()</a>.
         * getText() will return text that is inside the processing instruction.
         * Calls to next() will skip processing instructions automatically.
         * @see #nextToken
         * @see #getText
         */
        public int PROCESSING_INSTRUCTION { get; } = 8;

        /**
         * An XML comment was just read. This event type is this token is
         * available via <a href="#nextToken()">nextToken()</a> only;
         * calls to next() will skip comments automatically.
         * The content of the comment can be accessed using the getText()
         * method.
         *
         * @see #nextToken
         * @see #getText
         */
        public int COMMENT { get; } = 9;

        /**
         * An XML document type declaration was just read. This token is
         * available from <a href="#nextToken()">nextToken()</a> only.
         * The unparsed text inside the doctype is available via
         * the getText() method.
         *
         * @see #nextToken
         * @see #getText
         */
        public int DOCDECL { get; } = 10;

        /**
         * This array can be used to convert the event type integer constants
         * such as START_TAG or TEXT to
         * to a string. For example, the value of TYPES[START_TAG] is
         * the string "START_TAG".
         *
         * This array is intended for diagnostic output only. Relying
         * on the contents of the array may be dangerous since malicous
         * applications may alter the array, although it is final, due
         * to limitations of the Java language.
         */
        public string[] TYPES { get; } = {
        "START_DOCUMENT",
            "END_DOCUMENT",
            "START_TAG",
            "END_TAG",
            "TEXT",
            "CDSECT",
            "ENTITY_REF",
            "IGNORABLE_WHITESPACE",
            "PROCESSING_INSTRUCTION",
            "COMMENT",
            "DOCDECL"
        };

        // ----------------------------------------------------------------------------
        // namespace related features

        /**
         * This feature determines whether the parser processes
         * namespaces. As for all features, the default value is false.
         * <p><strong>NOTE:</strong> The value can not be changed during
         * parsing an must be set before parsing.
         *
         * @see #getFeature
         * @see #setFeature
         */
        public string FEATURE_PROCESS_NAMESPACES { get; } = "https://xmlpull.org/v1/doc/features.html#process-namespaces";

        /**
         * This feature determines whether namespace attributes are
         * exposed via the attribute access methods. Like all features,
         * the default value is false. This feature cannot be changed
         * during parsing.
         *
         * @see #getFeature
         * @see #setFeature
         */
        public string FEATURE_REPORT_NAMESPACE_ATTRIBUTES { get; } = "https://xmlpull.org/v1/doc/features.html#report-namespace-prefixes";

        /**
         * This feature determines whether the document declaration
         * is processed. If set to false,
         * the DOCDECL event type is reported by nextToken()
         * and ignored by next().
         *
         * If this featue is activated, then the document declaration
         * must be processed by the parser.
         *
         * <p><strong>Please note:</strong> If the document type declaration
         * was ignored, entity references may cause exceptions
         * later in the parsing process.
         * The default value of this feature is false. It cannot be changed
         * during parsing.
         *
         * @see #getFeature
         * @see #setFeature
         */
        public string FEATURE_PROCESS_DOCDECL { get; } = "https://xmlpull.org/v1/doc/features.html#process-docdecl";

        /**
         * If this feature is activated, all validation errors as
         * defined in the XML 1.0 sepcification are reported.
         * This implies that FEATURE_PROCESS_DOCDECL is true and both, the
         * internal and external document type declaration will be processed.
         * <p><strong>Please Note:</strong> This feature can not be changed
         * during parsing. The default value is false.
         *
         * @see #getFeature
         * @see #setFeature
         */
        public string FEATURE_VALIDATION { get; } = "https://xmlpull.org/v1/doc/features.html#validation";

        /**
         * Use this call to change the general behaviour of the parser,
         * such as namespace processing or doctype declaration handling.
         * This method must be called before the first call to next or
         * nextToken. Otherwise, an exception is thrown.
         * <p>Example: call setFeature(FEATURE_PROCESS_NAMESPACES, true) in order
         * to switch on namespace processing. The initial settings correspond
         * to the properties requested from the XML Pull Parser factory.
         * If none were requested, all feautures are deactivated by default.
         *
         * @exception XmlPullParserException If the feature is not supported or can not be set
         * @exception IllegalArgumentException If string with the feature name is null
         */
        public void SetFeature(string name, bool state) => throw new XmlPullParserException(E_NOT_SUPPORTED);

        /**
         * Returns the current value of the given feature.
         * <p><strong>Please note:</strong> unknown features are
         * <strong>always</strong> returned as false.
         *
         * @param name The name of feature to be retrieved.
         * @return The value of the feature.
         * @exception IllegalArgumentException if string the feature name is null
         */
        public bool GetFeature(string name) { return false; }

        /**
         * Set the value of a property.
         *
         * The property name is any fully-qualified URI.
         *
         * @exception XmlPullParserException If the property is not supported or can not be set
         * @exception IllegalArgumentException If string with the property name is null
         */
        public void SetProperty(string name, object value) => throw new XmlPullParserException(E_NOT_SUPPORTED);

        /**
         * Look up the value of a property.
         *
         * The property name is any fully-qualified URI.
         * <p><strong>NOTE:</strong> unknown properties are <strong>always</strong>
         * returned as null.
         *
         * @param name The name of property to be retrieved.
         * @return The value of named property.
         */
        public object GetProperty(string name) {  return null; }

        /**
         * Set the input source for parser to the given reader and
         * resets the parser. The event type is set to the initial value
         * START_DOCUMENT.
         * Setting the reader to null will just stop parsing and
         * reset parser state,
         * allowing the parser to free internal resources
         * such as parsing buffers.
         */
        public void SetInput(TextReader input) => throw new XmlPullParserException(E_NOT_SUPPORTED);

        /**
         * Sets the input stream the parser is going to process.
         * This call resets the parser state and sets the event type
         * to the initial value START_DOCUMENT.
         *
         * <p><strong>NOTE:</strong> If an input encoding string is passed,
         *  it MUST be used. Otherwise,
         *  if inputEncoding is null, the parser SHOULD try to determine
         *  input encoding following XML 1.0 specification (see below).
         *  If encoding detection is supported then following feature
         *  <a href="http://xmlpull.org/v1/doc/features.html#detect-encoding">http://xmlpull.org/v1/doc/features.html#detect-encoding</a>
         *  MUST be true amd otherwise it must be false
         *
         * @param inputStream contains a raw byte input stream of possibly
         *     unknown encoding (when inputEncoding is null).
         *
         * @param inputEncoding if not null it MUST be used as encoding for inputStream
         */
        public void SetInput(Stream inputStream, string inputEncoding) => throw new XmlPullParserException(E_NOT_SUPPORTED);

        /**
         * Returns the input encoding if known, null otherwise.
         * If setInput(InputStream, inputEncoding) was called with an inputEncoding
         * value other than null, this value must be returned
         * from this method. Otherwise, if inputEncoding is null and
         * the parser suppports the encoding detection feature
         * (http://xmlpull.org/v1/doc/features.html#detect-encoding),
         * it must return the detected encoding.
         * If setInput(Reader) was called, null is returned.
         * After first call to next if XML declaration was present this method
         * will return encoding declared.
         */
        public string GetInputEncoding() { return null; }

        /**
         * Set new value for entity replacement text as defined in
         * <a href="http://www.w3.org/TR/REC-xml#intern-replacement">XML 1.0 Section 4.5
         * Construction of Internal Entity Replacement Text</a>.
         * If FEATURE_PROCESS_DOCDECL or FEATURE_VALIDATION are set, calling this
         * function will result in an exception -- when processing of DOCDECL is
         * enabled, there is no need to the entity replacement text manually.
         *
         * <p>The motivation for this function is to allow very small
         * implementations of XMLPULL that will work in J2ME environments.
         * Though these implementations may not be able to process the document type
         * declaration, they still can work with known DTDs by using this function.
         *
         * <p><b>Please notes:</b> The given value is used literally as replacement text
         * and it corresponds to declaring entity in DTD that has all special characters
         * escaped: left angle bracket is replaced with &amp;lt;, ampersnad with &amp;amp;
         * and so on.
         *
         * <p><b>Note:</b> The given value is the literal replacement text and must not
         * contain any other entity reference (if it contains any entity reference
         * there will be no further replacement).
         *
         * <p><b>Note:</b> The list of pre-defined entity names will
         * always contain standard XML entities such as
         * amp (&amp;amp;), lt (&amp;lt;), gt (&amp;gt;), quot (&amp;quot;), and apos (&amp;apos;).
         * Those cannot be redefined by this method!
         *
         * @see #setInput
         * @see #FEATURE_PROCESS_DOCDECL
         * @see #FEATURE_VALIDATION
         */
        public void DefineEntityReplacementText(string entityName, string replacementText) => throw new XmlPullParserException(E_NOT_SUPPORTED);

        /**
         * Returns the numbers of elements in the namespace stack for the given
         * depth.
         * If namespaces are not enabled, 0 is returned.
         *
         * <p><b>NOTE:</b> when parser is on END_TAG then it is allowed to call
         *  this function with getDepth()+1 argument to retrieve position of namespace
         *  prefixes and URIs that were declared on corresponding START_TAG.
         * <p><b>NOTE:</b> to retrieve lsit of namespaces declared in current element:<pre>
         *       XmlPullParser pp = ...
         *       int nsStart = pp.getNamespaceCount(pp.getDepth()-1);
         *       int nsEnd = pp.getNamespaceCount(pp.getDepth());
         *       for (int i = nsStart; i < nsEnd; i++) {
         *          String prefix = pp.getNamespacePrefix(i);
         *          String ns = pp.getNamespaceUri(i);
         *           // ...
         *      }
         * </pre>
         *
         * @see #getNamespacePrefix
         * @see #getNamespaceUri
         * @see #getNamespace()
         * @see #getNamespace(String)
         */
        public int GetNamespaceCount(int depth)
        {
            return m_namespaces.GetAccumulatedCount(depth);
        }

        /**
         * Returns the namespace prefixe for the given position
         * in the namespace stack.
         * Default namespace declaration (xmlns='...') will have null as prefix.
         * If the given index is out of range, an exception is thrown.
         * <p><b>Please note:</b> when the parser is on an END_TAG,
         * namespace prefixes that were declared
         * in the corresponding START_TAG are still accessible
         * although they are no longer in scope.
         */
        public string GetNamespacePrefix(int pos)
        {
            int prefix = m_namespaces.GetPrefix(pos);
            return m_strings.GetString(prefix);
        }

        /**
         * Returns the namespace URI for the given position in the
         * namespace stack
         * If the position is out of range, an exception is thrown.
         * <p><b>NOTE:</b> when parser is on END_TAG then namespace prefixes that were declared
         *  in corresponding START_TAG are still accessible even though they are not in scope
         */
        public string GetNamespaceUri(int pos)
        {
            int uri = m_namespaces.GetUri(pos);
            return m_strings.GetString(uri);
        }

        /**
         * Returns the URI corresponding to the given prefix,
         * depending on current state of the parser.
         *
         * <p>If the prefix was not declared in the current scope,
         * null is returned. The default namespace is included
         * in the namespace table and is available via
         * getNamespace (null).
         *
         * <p>This method is a convenience method for
         *
         * <pre>
         *  for (int i = getNamespaceCount(getDepth ())-1; i >= 0; i--) {
         *   if (getNamespacePrefix(i).equals( prefix )) {
         *     return getNamespaceUri(i);
         *   }
         *  }
         *  return null;
         * </pre>
         *
         * <p><strong>Please note:</strong> parser implementations
         * may provide more efifcient lookup, e.g. using a Hashtable.
         * The 'xml' prefix is bound to "http://www.w3.org/XML/1998/namespace", as
         * defined in the
         * <a href="http://www.w3.org/TR/REC-xml-names/#ns-using">Namespaces in XML</a>
         * specification. Analogous, the 'xmlns' prefix is resolved to
         * <a href="http://www.w3.org/2000/xmlns/">http://www.w3.org/2000/xmlns/</a>
         *
         * @see #getNamespaceCount
         * @see #getNamespacePrefix
         * @see #getNamespaceUri
         */
        public string GetNamespace(string prefix) => throw new RuntimeException(E_NOT_SUPPORTED);

        public string GetPositionDescription();

        public int GetColumnNumber();

        public bool IsWhitespace();

        public string GetText();

        public char[] GetTextCharacters(out int start);

        public string GetNamespace();

        public string GetName();

        public string GetPrefix();

        public bool IsEmptyElementTag();

        public int GetAttributeCount();

        public string GetAttributeNamespace(int index);

        public string GetAttributeName(int index);

        public string GetAttributePrefix(int index);

        public string GetAttributeType(int index);

        public bool IsAttributeDefault(int index);

        public string GetAttributeValue(int index);

        public string GetAttributeValue(string namespaceUri, string name);

        public int GetEventType();

        public int Next();

        public int NextToken();

        public void Require(int type, string namespaceUri, string name);

        public string NextText();

        public int NextTag();
    }
}