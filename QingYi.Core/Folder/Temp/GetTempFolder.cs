using System.IO;

namespace QingYi.Core.Folder.Temp
{
    public class GetTempFolder
    {
        /// <summary>
        /// Get the Temp folder<br></br>
        /// 获取Temp文件夹
        /// </summary>
        /// <returns>
        ///     Temp folder|Temp文件夹路径
        /// </returns>
        public static string Get()
        {
            return Path.GetTempPath();
        }
    }
}