using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace QingYi.AXML.Android.XmlPull.V1
{
    /**
     * This class is used to create implementations of XML Pull Parser defined in XMPULL V1 API.
     * The name of actual factory class will be determined based on several parameters.
     * It works similar to JAXP but tailored to work in J2ME environments
     * (no access to system properties or file system) so name of parser class factory to use
     * and its class used for loading (no class loader - on J2ME no access to context class loaders)
     * must be passed explicitly. If no name of parser factory was passed (or is null)
     * it will try to find name by searching in CLASSPATH for
     * META-INF/services/XmlPullParserFactory resource that should contain
     * a comma separated list of class names of factories or parsers to try (in order from
     * left to the right). If none found, it will throw an exception.
     *
     * <br /><strong>NOTE:</strong>In J2SE or J2EE environments, you may want to use
     * <code>newInstance(property, classLoaderCtx)</code>
     * where first argument is
     * <code>System.getProperty(XmlPullParserFactory.PROPERTY_NAME)</code>
     * and second is <code>Thread.getContextClassLoader().getClass()</code> .
     *
     * @see XmlPullParser
     *
     * @author <a href="http://www.extreme.indiana.edu/~aslom/">Aleksander Slominski</a>
     * @author Stefan Haustein
     */
    public class XmlPullParserFactory
    {
        XmlPullParser xmlPullParser = new XmlPullParser();

        /** used as default class to server as context class in newInstance() */
        private static readonly Type referenceContextClass;

        static XmlPullParserFactory()
        {
            XmlPullParserFactory f = new XmlPullParserFactory();
            referenceContextClass = f.GetType();
        }

        /** Name of the system or midlet property that should be used for
         a system property containing a comma separated list of factory
         or parser class names (value: XmlPullParserFactory). */

        public const string PROPERTY_NAME = "XmlPullParserFactory";
        private const string RESOURCE_NAME = "/META-INF/services/" + PROPERTY_NAME;

        // public const string DEFAULT_PROPERTY = "org.xmlpull.xpp3.XmlPullParser,org.kxml2.io.KXmlParser";

        protected List<Type> parserClasses;
        protected string classNamesLocation;
        protected List<Type> serializerClasses;
        protected Hashtable features = new Hashtable();

        /**
         * Protected constructor to be called by factory implementations.
         */
        protected XmlPullParserFactory() { }

        /**
         * Set the features to be set when XML Pull Parser is created by this factory.
         * <p><b>NOTE:</b> factory features are not used for XML Serializer.
         *
         * @param name string with URI identifying feature
         * @param state if true feature will be set; if false will be ignored
         */
        public void SetFeature(string name, bool state) => features[name] = state;

        /**
         * Return the current value of the feature with given name.
         * <p><b>NOTE:</b> factory features are not used for XML Serializer.
         *
         * @param name The name of feature to be retrieved.
         * @return The value of named feature.
         *     Unknown features are <string>always</strong> returned as false
         */
        public bool GetFeature(string name)
        {
            bool result = false;
            if (features.ContainsKey(name))
            {
                bool? value = features[name] as bool?;
                result = value ?? false;
            }
            return result;
        }

        /**
         * Specifies that the parser produced by this factory will provide
         * support for XML namespaces.
         * By default the value of this is set to false.
         *
         * @param awareness true if the parser produced by this code
         *    will provide support for XML namespaces;  false otherwise.
         */
        public void SetNamespaceAware(bool awareness) => features[xmlPullParser.FEATURE_PROCESS_NAMESPACES] = awareness;

        /**
         * Indicates whether or not the factory is configured to produce
         * parsers which are namespace aware
         * (it simply set feature XmlPullParser.FEATURE_PROCESS_NAMESPACES to true or false).
         *
         * @return  true if the factory is configured to produce parsers
         *    which are namespace aware; false otherwise.
         */
        public bool IsNamespaceAware() { return GetFeature(xmlPullParser.FEATURE_PROCESS_NAMESPACES); }

        /**
         * Specifies that the parser produced by this factory will be validating
         * (it simply set feature XmlPullParser.FEATURE_VALIDATION to true or false).
         *
         * By default the value of this is set to false.
         *
         * @param validating - if true the parsers created by this factory  must be validating.
         */
        public void SetValidating(bool validating) => features[xmlPullParser.FEATURE_VALIDATION] = validating;

        /**
         * Indicates whether or not the factory is configured to produce parsers
         * which validate the XML content during parse.
         *
         * @return   true if the factory is configured to produce parsers
         * which validate the XML content during parse; false otherwise.
         */
        public bool IsValidating() { return GetFeature(xmlPullParser.FEATURE_VALIDATION); }

        /**
         * Creates a new instance of a XML Pull Parser
         * using the currently configured factory features.
         *
         * @return A new instance of a XML Pull Parser.
         * @throws XmlPullParserException if a parser cannot be created which satisfies the
         * requested configuration.
         */
        public XmlPullParser NewPullParser()
        {
            if (parserClasses == null) throw new XmlPullParserException("Factory initialization was incomplete - has not tried " + classNamesLocation);

            if (parserClasses.Count == 0) throw new XmlPullParserException("No valid parser classes found in " + classNamesLocation);

            StringBuilder issues = new StringBuilder();

            for (int i = 0; i < parserClasses.Count; i++)
            {
                Type ppClass = parserClasses[i];
                try
                {
                    XmlPullParser pp = (XmlPullParser)Activator.CreateInstance(ppClass);

                    // Java commented-out code:
                    // if( ! features.isEmpty() ) {
                    // Enumeration keys = features.keys();
                    // while(keys.hasMoreElements()) {

                    foreach (object key in features.Keys)
                    {
                        string featureKey = (string)key;
                        bool? value = (bool?)features[key];
                        if (value != null && value.Value)
                        {
                            pp.SetFeature(featureKey, true);
                        }
                    }
                    return pp;
                }
                catch (System.Exception ex)
                {
                    issues.Append(ppClass.FullName + ": " + ex.ToString() + "; ");
                }
            }

            throw new XmlPullParserException("Could not create parser: " + issues);
        }

        /**
         * Creates a new instance of a XML Serializer.
         *
         * <p><b>NOTE:</b> factory features are not used for XML Serializer.
         *
         * @return A new instance of a XML Serializer.
         * @throws XmlPullParserException if a parser cannot be created which satisfies the
         * requested configuration.
         */
        public XmlSerializer NewSerializer()
        {
            if (serializerClasses == null)
            {
                throw new XmlPullParserException(
                    "Factory initialization incomplete - has not tried " + classNamesLocation);
            }
            if (serializerClasses.Count == 0)
            {
                throw new XmlPullParserException(
                    "No valid serializer classes found in " + classNamesLocation);
            }

            StringBuilder issues = new StringBuilder();

            for (int i = 0; i < serializerClasses.Count; i++)
            {
                Type ppClass = serializerClasses[i];
                try
                {
                    XmlSerializer ser = (XmlSerializer)Activator.CreateInstance(ppClass);

                    // Java commented-out code:
                    // for (Enumeration e = features.keys (); e.hasMoreElements ();) {
                    //     String key = (String) e.nextElement();
                    //     Boolean value = (Boolean) features.get(key);
                    //     if(value != null && value.booleanValue()) {
                    //         ser.setFeature(key, true);
                    //     }
                    // }

                    return ser;
                }
                catch (System.Exception ex)
                {
                    issues.Append(ppClass.FullName + ": " + ex.ToString() + "; ");
                }
            }

            throw new XmlPullParserException("Could not create serializer: " + issues);
        }
    }
}
