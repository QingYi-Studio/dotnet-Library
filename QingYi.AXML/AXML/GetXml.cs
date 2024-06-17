using QingYi.AXML.Android.Content;
using QingYi.AXML.Android.Util;
using System;
using System.Threading.Tasks;

namespace AXML
{
    public class GetXml
    {
        private readonly bool addXmlHead = true;
        private readonly string inputFilePath;
        private readonly string outputFilePath;

        public GetXml(bool addXmlHead, string inputFilePath, string outputFilePath)
        {
            this.addXmlHead = addXmlHead;
            this.inputFilePath = inputFilePath;
            this.outputFilePath = outputFilePath;
        }

        public async Task<string> GetAsync()
        {
            // TODO
            return null;
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
