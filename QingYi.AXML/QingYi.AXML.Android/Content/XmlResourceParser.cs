namespace QingYi.AXML.Android.Content
{
    // 定义一个接口，继承自 XmlPullParser 和 AttributeSet
    public interface IXmlResourceParser : XmlPullParser, AttributeSet
    {
        // 在接口中声明 close() 方法
        void Close();
    }
}
