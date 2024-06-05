using SIO = System.IO;

namespace QingYi.Core.Folder
{
    public class Display
    {
        /// <summary>
        /// Show specified folder<br></br>
        /// 显示指定文件夹
        /// </summary>
        /// <param name="folderPath">
        ///     The name of the folder you want to show<br></br>
        ///     想要显示的文件夹名称
        /// </param>
        /// <exception cref="Exception"></exception>
        public static void SetDisplay(string folderPath)
        {
            try
            {
                // 设置文件夹属性为显示即清除文件夹的隐藏属性
                SIO.File.SetAttributes(folderPath, SIO.File.GetAttributes(folderPath) & ~FileAttributes.Hidden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
