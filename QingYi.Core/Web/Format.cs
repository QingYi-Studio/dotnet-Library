using System.Text.RegularExpressions;
using System;

namespace QingYi.Core.Web
{
    public partial class Format
    {
        /// <summary>
        /// Extract domain name separately<br></br>
        /// 单独提取域名
        /// </summary>
        /// <param name="url">
        ///     Url<br></br>
        ///     网址
        /// </param>
        /// <returns></returns>
        public static string GetHost(string url)
        {
            // 解析URL
            Uri uri = new(url);

            // 获取主机名
            string hostname = uri.Host;

            return hostname;
        }

        /// <summary>
        /// Extract a domain name without www<br></br>
        /// 提取无www的域名
        /// </summary>
        /// <param name="url">
        ///     Url<br></br>
        ///     网址
        /// </param>
        /// <returns></returns>
        public static string GetHostRemoveWWW(string url)
        {
            // 解析URL
            Uri uri = new(url);

            // 获取主机名
            string hostname = uri.Host;

            // 移除前缀（www.）
            hostname = hostname.Replace("www.", "");

            return hostname;
        }

        /// <summary>
        /// Get root domain<br></br>
        /// 获取根域名
        /// </summary>
        /// <param name="url">
        ///     Url<br></br>
        ///     网址
        /// </param>
        /// <returns></returns>
        public static string GetDomain(string url)
        {
            // 解析URL
            Uri uri = new(url);

            // 获取主机名
            string hostname = uri.Host;

            // 匹配根域名
            Match match = GetDomain().Match(hostname);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return hostname; // 返回原始主机名
            }
        }

        [GeneratedRegex(@"(?:(?:[\w-]+\.)+)?([\w-]+\.[\w-]+)$")]
        private static partial Regex GetDomain();
    }
}
