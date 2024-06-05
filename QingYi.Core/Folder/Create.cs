namespace QingYi.Core.Folder
{
    public class Create
    {
        /// <summary>
        /// Creatr Folder<br></br>
        /// 创建文件夹
        /// </summary>
        /// <param name="folderName">
        ///     The name of the folder you want to create<br></br>
        ///     你想创建的文件夹名称
        /// </param>
        /// <param name="parentFolderPath">
        ///     Root folder name. If this parameter is not specified, the default directory is the current directory<br></br>
        ///     根文件夹名称，如果不填写则默认当前目录
        /// </param>
        public static void CreateFolder(string folderName, string? parentFolderPath = null)
        {
            try
            {
                // 如果未提供父文件夹路径，则使用当前目录
                if (string.IsNullOrEmpty(parentFolderPath))
                {
                    parentFolderPath = Directory.GetCurrentDirectory();
                }
                else if (!Path.IsPathRooted(parentFolderPath))
                {
                    // 如果提供的路径不是绝对路径，则将其与当前目录组合
                    parentFolderPath = Path.Combine(Directory.GetCurrentDirectory(), parentFolderPath);
                }

                // 组合路径
                string newFolderPath = Path.Combine(parentFolderPath, folderName);

                // 创建文件夹，如果路径中包含不存在的文件夹，则会自动创建这些文件夹
                Directory.CreateDirectory(newFolderPath);
                Console.WriteLine("文件夹创建成功：" + newFolderPath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
