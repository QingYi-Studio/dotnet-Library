using QingYi.AXML.Android.Util;
using QingYi.AXML.Android.XmlPull.V1;

namespace QingYi.AXML.Android.Content
{
    // 定义一个接口，继承自 XmlPullParser 和 AttributeSet
    public interface IXmlResourceParser : IXmlPullParser, IAttributeSet
    {
        // 在接口中声明 close() 方法
        void Close();
    }
}
