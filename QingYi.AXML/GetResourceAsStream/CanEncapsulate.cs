using System.Collections.Generic;

namespace QingYi.AXML.GetResourceAsStream
{
    internal class Encapsulate
    {
        public static bool CanEncapsulate(string name)
        {
            int len = name.Length;
            if (len > 6 && name.EndsWith(".class"))
            {
                return false;
            }
            else
            {
                return IsPackageName(ToPackageName(name));
            }
        }

        private static bool IsPackageName(string name)
        {
            return IsTypeName(name);
        }

        private static string ToPackageName(string name)
        {
            int index = name.LastIndexOf('/');
            if (index == -1 || index == name.Length - 1)
            {
                return "";
            }
            else
            {
                return name.Substring(0, index).Replace('/', '.');
            }
        }

        private static bool IsTypeName(string name)
        {
            int next;
            int off = 0;
            while ((next = name.IndexOf('.', off)) != -1)
            {
                string id = name.Substring(off, next - off);
                if (!IsJavaIdentifier(id))
                    return false;
                off = next + 1;
            }
            string last = name.Substring(off);
            return IsJavaIdentifier(last);
        }

        private static bool IsJavaIdentifier(string str)
        {
            if (string.IsNullOrEmpty(str) || IsReserved(str))
                return false;

            int first = char.ConvertToUtf32(str, 0);
            if (!char.IsLetter((char)first) && first != '_' && first != '$')
                return false;

            int i = char.IsSurrogatePair(str, 0) ? 2 : 1;
            while (i < str.Length)
            {
                int cp = char.ConvertToUtf32(str, i);
                if (!char.IsLetterOrDigit((char)cp) && cp != '_' && cp != '$')
                    return false;
                i += char.IsSurrogatePair(str, i) ? 2 : 1;
            }

            return true;
        }

        private static bool IsReserved(string str)
        {
            return RESERVED.Contains(str);
        }

        private static readonly HashSet<string> RESERVED = new HashSet<string>
        {
            "abstract",
            "assert",
            "boolean",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "class",
            "const",
            "continue",
            "default",
            "do",
            "double",
            "else",
            "enum",
            "extends",
            "final",
            "finally",
            "float",
            "for",
            "goto",
            "if",
            "implements",
            "import",
            "instanceof",
            "int",
            "interface",
            "long",
            "native",
            "new",
            "package",
            "private",
            "protected",
            "public",
            "return",
            "short",
            "static",
            "strictfp",
            "super",
            "switch",
            "synchronized",
            "this",
            "throw",
            "throws",
            "transient",
            "try",
            "void",
            "volatile",
            "while",
            "true",
            "false",
            "null",
            "_"
        };
    }
}
