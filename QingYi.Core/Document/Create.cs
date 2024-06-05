namespace QingYi.Core.Document
{
    public class Create
    {
        /// <summary>
        /// Create file<br></br>
        /// 创建文件
        /// </summary>
        /// <param name="filePath">
        ///     The file you want to create(full path)<br></br>
        ///     你想要创建的文件（完整路径）
        /// </param>
        /// <exception cref="Exception"></exception>
        public static void CreateFile(string filePath)
        {
            try
            {
                // 创建空文件
                File.Create(filePath).Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
                    CreateFile(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
