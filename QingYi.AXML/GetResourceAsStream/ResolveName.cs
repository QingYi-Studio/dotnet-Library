using System;
using System.Runtime.CompilerServices;

namespace QingYi.AXML.GetResourceAsStream
{
    internal class ResolveName
    {
        public string Resolve(string name)
        {
            if (!name.StartsWith("/"))
            {
                Type c = IsArray() ? ElementType() : this.GetType();
                string baseName = c.Namespace;
                if (!string.IsNullOrEmpty(baseName))
                {
                    name = baseName.Replace('.', '/') + "/" + name;
                }
            }
            else
            {
                name = name.Substring(1);
            }
            return name;
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool IsArray();

        private Type ElementType()
        {
            if (!IsArray()) return null;

            Type c = GetType(); // Equivalent to typeof(MyClass) if this method is within MyClass

            while (c.IsArray)
            {
                c = c.GetElementType();
            }

            return c;
        }
    }
}
