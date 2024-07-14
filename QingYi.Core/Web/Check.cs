using System.Threading.Tasks;

namespace QingYi.Core.Web
{
    public class Check
    {
        public static async Task<double> CheckWebsiteAsync(string url)
        {
            // 格式化URL，去除协议和路径
            string formattedUrl = Format.GetHost(url);

            // 发起四次连接测试，并记录延迟
            double totalDelay = 0;
            for (int i = 0; i < 4; i++)
            {
                totalDelay += await Test.TestConnectionAsync(formattedUrl);
            }

            // 计算平均延迟
            double averageDelay = totalDelay / 4;

            return averageDelay;
        }
    }
}
