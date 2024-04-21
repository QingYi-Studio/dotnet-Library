namespace Picture.AXmlToSvg
{
    public class StartConvert
    {
        /// <summary>
        /// 同步启动
        /// </summary>
        /// <param name="inputFilePath">输入文件完整路径</param>
        /// <param name="outputFilePath">输出文件路径</param>
        public static void Start(string inputFilePath, string outputFilePath)
        {
            // 创建导出文件
            CreateFile.Create(outputFilePath);

            // 转换
            string output = Convert.ConvertSync(inputFilePath);

            // 写入
            Write.WriteSync(outputFilePath, output);
        }

        /// <summary>
        /// 异步启动
        /// </summary>
        /// <param name="inputFilePath">输入文件完整路径</param>
        /// <param name="outputFilePath">输出文件路径</param>
        public static async Task StartAsync(string inputFilePath, string outputFilePath)
        {
            // 创建导出文件
            await CreateFile.CreateAsync(outputFilePath);

            // 转换
            string output = await Convert.ConvertAsync(inputFilePath);

            // 写入
            await Write.WriteAsync(outputFilePath, output);
        }
    }
}
