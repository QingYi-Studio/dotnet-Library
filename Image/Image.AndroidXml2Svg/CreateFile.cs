namespace Image.AndroidXml2Svg
{
    internal class CreateFile
    {
        /// <summary>
        /// 根据文件路径同步创建对应文件
        /// </summary>
        /// <param name="filePath">完整文件路径</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void Create(string filePath)
        {
            try
            {
                // 检查文件路径是否为空
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentNullException(nameof(filePath), "文件路径不能为空");
                }

                // 确保文件路径合法
                string directory = Path.GetDirectoryName(filePath)!;
                if (!Directory.Exists(directory))
                {
                    // 创建目录
                    Directory.CreateDirectory(directory);
                }

                // 创建文件
                using FileStream fs = File.Create(filePath);
                Console.WriteLine($"文件已创建：{filePath}");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// 根据文件路径异步创建对应文件
        /// </summary>
        /// <param name="filePath">完整文件路径</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task CreateAsync(string filePath)
        {
            try
            {
                // 检查文件路径是否为空
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentNullException(nameof(filePath), "文件路径不能为空");
                }

                // 确保文件路径合法
                string directory = Path.GetDirectoryName(filePath)!;
                if (!Directory.Exists(directory))
                {
                    // 创建目录
                    Directory.CreateDirectory(directory);
                }

                // 创建文件
                using FileStream fs = File.Create(filePath);
                Console.WriteLine($"文件已创建：{filePath}");
                await Task.CompletedTask; // 异步等待
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
