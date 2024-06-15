namespace QingYi.AXML.Android.Util
{
    public class TypedValue
    {
        public int type;
        public string str = null;

        // Additional fields
        public int data;
        public int assetCookie;
        public int resourceId;
        public int changingConfigurations;

        // Constants equivalent to Java's static final int
        public const int TYPE_NULL = 0;
        public const int TYPE_REFERENCE = 1;
        public const int TYPE_ATTRIBUTE = 2;
        public const int TYPE_STRING = 3;
        public const int TYPE_FLOAT = 4;
        public const int TYPE_DIMENSION = 5;
        public const int TYPE_FRACTION = 6;
        public const int TYPE_FIRST_INT = 16;
        public const int TYPE_INT_DEC = 16;
        public const int TYPE_INT_HEX = 17;
        public const int TYPE_INT_BOOLEAN = 18;
        public const int TYPE_FIRST_COLOR_INT = 28;
        public const int TYPE_INT_COLOR_ARGB8 = 28;
        public const int TYPE_INT_COLOR_RGB8 = 29;
        public const int TYPE_INT_COLOR_ARGB4 = 30;
        public const int TYPE_INT_COLOR_RGB4 = 31;
        public const int TYPE_LAST_COLOR_INT = 31;
        public const int TYPE_LAST_INT = 31;

        // Complex unit constants
        public const int COMPLEX_UNIT_PX = 0;
        public const int COMPLEX_UNIT_DIP = 1;
        public const int COMPLEX_UNIT_SP = 2;
        public const int COMPLEX_UNIT_PT = 3;
        public const int COMPLEX_UNIT_IN = 4;
        public const int COMPLEX_UNIT_MM = 5;
        public const int COMPLEX_UNIT_SHIFT = 0;
        public const int COMPLEX_UNIT_MASK = 15;
        public const int COMPLEX_UNIT_FRACTION = 0;
        public const int COMPLEX_UNIT_FRACTION_PARENT = 1;
        public const int COMPLEX_RADIX_23p0 = 0;
        public const int COMPLEX_RADIX_16p7 = 1;
        public const int COMPLEX_RADIX_8p15 = 2;
        public const int COMPLEX_RADIX_0p23 = 3;
        public const int COMPLEX_RADIX_SHIFT = 4;
        public const int COMPLEX_RADIX_MASK = 3;
        public const int COMPLEX_MANTISSA_SHIFT = 8;
        public const int COMPLEX_MANTISSA_MASK = 0xFFFFFF;
    }
}
