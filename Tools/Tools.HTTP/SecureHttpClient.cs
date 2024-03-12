using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Tools.HTTP
{
    public class SecureHttpClient
    {
        private HttpClient _client;

        public SecureHttpClient(string certificateFilePath, string certificatePassword)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = ValidateServerCertificate!;

            var certificate = new X509Certificate2(certificateFilePath, certificatePassword);
            handler.ClientCertificates.Add(certificate);

            _client = new HttpClient(handler);
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        private static bool ValidateServerCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // 检查证书是否过期
            if (certificate.NotAfter < DateTime.Now)
            {
                // 证书已过期
                return false;
            }

            // 检查证书是否由可信任的CA签发
            X509ChainStatus[] chainStatus = chain.ChainStatus;
            bool isCertChainValid = true;
            foreach (X509ChainStatus status in chainStatus)
            {
                if (status.Status != X509ChainStatusFlags.NoError)
                {
                    isCertChainValid = false;
                    break;
                }
            }

            return isCertChainValid;
        }
    }
}
