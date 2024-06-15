namespace QingYi.AXML.Android.Util
{
    public interface IAttributeSet
    {
        int GetAttributeCount();
        string GetAttributeName(int index);
        string GetAttributeValue(int index);
        string GetPositionDescription();
        int GetAttributeNameResource(int index);
        int GetAttributeListValue(int index, string[] options, int defaultValue);
        bool GetAttributeBooleanValue(int index, bool defaultValue);
        int GetAttributeResourceValue(int index, int defaultValue);
        int GetAttributeIntValue(int index, int defaultValue);
        int GetAttributeUnsignedIntValue(int index, int defaultValue);
        float GetAttributeFloatValue(int index, float defaultValue);
        string GetIdAttribute();
        string GetClassAttribute();
        int GetIdAttributeResourceValue(int index);
        int GetStyleAttribute();
        string GetAttributeValue(string @namespace, string attribute);
        int GetAttributeListValue(string @namespace, string attribute, string[] options, int defaultValue);
        bool GetAttributeBooleanValue(string @namespace, string attribute, bool defaultValue);
        int GetAttributeResourceValue(string @namespace, string attribute, int defaultValue);
        int GetAttributeIntValue(string @namespace, string attribute, int defaultValue);
        int GetAttributeUnsignedIntValue(string @namespace, string attribute, int defaultValue);
        float GetAttributeFloatValue(string @namespace, string attribute, float defaultValue);

        // TODO: remove
        int GetAttributeValueType(int index);
        int GetAttributeValueData(int index);
    }
}
