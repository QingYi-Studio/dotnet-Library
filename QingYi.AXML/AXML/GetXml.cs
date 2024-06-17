using QingYi.AXML.Android.Content;
using QingYi.AXML.Android.Util;
using QingYi.AXML.Android.XmlPull.V1;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AXML
{
    public class GetXml
    {
        private readonly bool addXmlHead = true;

        public GetXml(bool addXmlHead)
        {
            this.addXmlHead = addXmlHead;
        }

        public Task<string> GetAsync(string[] arguments)
        {
            AXmlResourceParser parser = new AXmlResourceParser();
            XmlPullParser xmlPullParser = new XmlPullParser();
            StringBuilder output = new StringBuilder(); // 用于存储输出的字符串
            try
            {
                using (FileStream fileStream = new FileStream(arguments[0], FileMode.Open))
                {
                    parser.Open(fileStream);
                    StringBuilder indent = new StringBuilder(10);
                    const string indentStep = "\t";

                    while (true)
                    {
                        int type = parser.Next();
                        if (type == xmlPullParser.END_DOCUMENT)
                        {
                            break;
                        }

                        if (type == xmlPullParser.START_DOCUMENT)
                        {
                            if (addXmlHead == true)
                            {
                                output.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                            }
                        }
                        else if (type == xmlPullParser.START_TAG)
                        {
                            if (parser.GetEventType() == xmlPullParser.START_TAG)
                            {
                                string name = parser.GetName();
                                if (name != null)
                                {
                                    output.Append($"{indent}<{GetNamespacePrefix(parser.GetPrefix())}{name}");

                                    indent.Append(indentStep);
                                }
                                else
                                {
                                    output.AppendLine("Element name is null");
                                }
                            }
                            else
                            {
                                output.AppendLine("Parser is null or not positioned on a start tag");
                            }

                            int namespaceCountBefore = parser.GetNamespaceCount(parser.GetDepth() - 1);
                            int namespaceCount = parser.GetNamespaceCount(parser.GetDepth());

                            for (int i = namespaceCountBefore; i != namespaceCount; ++i)
                            {
                                output.AppendLine($"{indent}xmlns:{parser.GetNamespacePrefix(i)}=\"{parser.GetNamespaceUri(i)}\"");
                            }

                            for (int i = 0; i != parser.GetAttributeCount(); ++i)
                            {
                                output.AppendLine($"{indent}{GetNamespacePrefix(parser.GetAttributePrefix(i))}{parser.GetAttributeName(i)}=\"{GetAttributeValue(parser, i)}\"");
                            }

                            output.AppendLine($"{indent}>");
                        }
                        else if (type == xmlPullParser.END_TAG)
                        {
                            indent.Length -= indentStep.Length;
                            output.AppendLine($"{indent}</{GetNamespacePrefix(parser.GetPrefix())}{parser.GetName()}>");
                        }
                        else if (type == xmlPullParser.TEXT)
                        {
                            output.AppendLine($"{indent}{parser.GetText()}");
                        }
                    }
                }

                // 打印或者返回输出的字符串
                Console.WriteLine(output.ToString());
                // 或者直接返回 output.ToString()，取决于你的需求

                return Task.FromResult(output.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return Task.FromResult(e.ToString());
            }

        }

        private static string GetNamespacePrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return "";
            }
            return prefix + ":";
        }

        private static string GetAttributeValue(AXmlResourceParser parser, int index)
        {
            int type = parser.GetAttributeValueType(index);
            int data = parser.GetAttributeValueData(index);

            switch (type)
            {
                case TypedValue.TYPE_STRING:
                    return parser.GetAttributeValue(index);

                case TypedValue.TYPE_ATTRIBUTE:
                    return $"?{GetPackage(data)}{data:X8}";

                case TypedValue.TYPE_REFERENCE:
                    return $"@{GetPackage(data)}{data:X8}";

                case TypedValue.TYPE_FLOAT:
                    return IntBitsToSingle(data).ToString();

                case TypedValue.TYPE_INT_HEX:
                    return $"0x{data:X8}";

                case TypedValue.TYPE_INT_BOOLEAN:
                    return data != 0 ? "true" : "false";

                case TypedValue.TYPE_DIMENSION:
                    return ComplexToFloat(data) + DimensionUnits[data & TypedValue.COMPLEX_UNIT_MASK];

                case TypedValue.TYPE_FRACTION:
                    return ComplexToFloat(data) + FractionUnits[data & TypedValue.COMPLEX_UNIT_MASK];

                case var _ when type >= TypedValue.TYPE_FIRST_COLOR_INT && type <= TypedValue.TYPE_LAST_COLOR_INT:
                    return $"#{data:X8}";

                case var _ when type >= TypedValue.TYPE_FIRST_INT && type <= TypedValue.TYPE_LAST_INT:
                    return data.ToString();

                default:
                    return $"<0x{data:X}, type 0x{type:X2}>";
            }
        }

        private static object IntBitsToSingle(int data)
        {
            byte[] bytes = BitConverter.GetBytes(data);
            return BitConverter.ToSingle(bytes, 0);
        }

        private static string GetPackage(int id)
        {
            if ((id >> 24) == 1)
            {
                return "android:";
            }
            return "";
        }

        // 由于原软件使用控制台运行，故使用Log输出(即C#中的WriteLine)，但是现在改成了类库，保留这个不删，但是不做使用
        private static void Log(string format, params object[] arguments)
        {
            Console.WriteLine(format, arguments);
        }

        /////////////////////////////////// ILLEGAL STUFF, DONT LOOK :)
        public static float ComplexToFloat(int complex)
        {
            return (complex & 0xFFFFFF00) * RadixMults[(complex >> 4) & 3];
        }

        private static readonly float[] RadixMults = {
            0.00390625F, 3.051758E-005F, 1.192093E-007F, 4.656613E-010F
        };

        private static readonly string[] DimensionUnits = {
            "px", "dip", "sp", "pt", "in", "mm", "", ""
        };

        private static readonly string[] FractionUnits = {
            "%", "%p", "", "", "", "", "", ""
        };
    }
}
