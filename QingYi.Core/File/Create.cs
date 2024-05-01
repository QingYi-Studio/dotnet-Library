namespace QingYi.Core.File
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
                System.IO.File.Create(filePath).Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
