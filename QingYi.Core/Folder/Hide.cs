using SIO = System.IO;

namespace QingYi.Core.Folder
{
    public class Hide
    {
        /// <summary>
        /// Hide specified folder<br></br>
        /// 隐藏指定文件夹
        /// </summary>
        /// <param name="folderPath">
        ///     The name of the folder you want to hide<br></br>
        ///     想要隐藏的文件夹名称
        /// </param>
        /// <exception cref="Exception"></exception>
        public static void SetHidden(string folderPath)
        {
            try
            {
                // 设置文件夹属性为隐藏
                SIO.File.SetAttributes(folderPath, SIO.File.GetAttributes(folderPath) | FileAttributes.Hidden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
