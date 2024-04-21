namespace Image.AndroidXml2Svg
{
    internal class Write
    {
        /// <summary>
        /// 同步把转换好的内容写入文件
        /// </summary>
        /// <param name="filePath">要导出的文件名称路径</param>
        /// <param name="content">需要写入的文本</param>
        /// <exception cref="Exception"></exception>
        public static void WriteSync(string filePath, string content)
        {
            try
            {
                // 创建一个 StreamWriter 实例，用于写入文件
                using StreamWriter writer = new(filePath);
                // 将字符串写入文件
                writer.Write(content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 同步把转换好的内容写入文件
        /// </summary>
        /// <param name="filePath">要导出的文件名称路径</param>
        /// <param name="content">需要写入的文本</param>
        /// <exception cref="Exception"></exception>
        public static async Task WriteAsync(string filePath, string content)
        {
            try
            {
                // 创建一个 StreamWriter 实例，用于异步写入文件
                using StreamWriter writer = new(filePath);
                // 异步将字符串写入文件
                await writer.WriteAsync(content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
