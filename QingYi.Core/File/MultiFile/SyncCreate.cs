using QingYi.Core.File;

namespace QingYi.Core.File.MultiFile
{
    public class SyncCreate
    {
        /// <summary>
        /// Create multiple files<br></br>
        /// 创建多个文件
        /// </summary>
        /// <param name="filePaths">
        ///     All the file you want to create.<br></br>
        ///     所有你想创建的文件。<br></br>
        ///     <code>
        ///         string[] filePaths = { @"C:\Folder\abc.txt", @"D:\Folder\abc.txt", @"E:\Folder\abc.txt" };
        ///     </code>
        /// </param>
        /// <exception cref="Exception">Same as "System.IO.File.Create"'s error</exception>
        public static void CreateFiles(string[] filePaths)
        {
            try
            {
                foreach (string filePath in filePaths)
                {
                    Create.CreateFile(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
