using System;

namespace QingYi.AXML.Android.XmlPull.V1
{
    /**
     * This exception is thrown to signal XML Pull Parser related faults.
     *
     * author <a href="http://www.extreme.indiana.edu/~aslom/">Aleksander Slominski</a>
     */
    public class XmlPullParserException : System.Exception
    {
        protected System.Exception detail;
        protected int row = -1;
        protected int column = -1;

        public XmlPullParserException(string message) : base(message)
        {
        }

        public XmlPullParserException(string message, XmlPullParser parser, System.Exception chain)
            : base($"{(message == null ? "" : message + " ")}"
                   + $"{(parser == null ? "" : $"(position:{parser.GetPositionDescription()}) ")}"
                   + $"{(chain == null ? "" : $"caused by: {chain}")}")
        {
            if (parser != null)
            {
                row = parser.GetLineNumber();
                column = parser.GetColumnNumber();
            }
            detail = chain;
        }

        public System.Exception Detail => detail;

        public int Row => row;

        public int Column => column;

        public override string ToString()
        {
            if (detail == null)
            {
                return base.ToString();
            }
            else
            {
                return $"{base.ToString()}; nested exception is:\n\t{detail.ToString()}";
            }
        }

        // NOTE: Printing stack trace is different in C#, so implementing only ToString for simplicity
    }
}
