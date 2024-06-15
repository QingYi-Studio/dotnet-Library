using System.IO;

namespace QingYi.AXML.Android.Content
{
    public static class ChunkUtil
    {
        public static void ReadCheckType(IntReader reader, int expectedType)
        {
            int type = reader.ReadInt();
            if (type != expectedType)
            {
                throw new IOException(
                    $"Expected chunk of type 0x{expectedType:X}, read 0x{type:X}.");
            }
        }
    }
}
