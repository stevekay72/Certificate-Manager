using CertificateServices.Interfaces;
using CertificateServices.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CertificateServices.Services
{
    public class CertificateReader : ICertificateReader
    {
        public async Task<CertificateInfo> GetCertificateInfo(Uri url)
        {
            X509Certificate2 certificate = null;
            var httpClientHandler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                ServerCertificateCustomValidationCallback = (_, cert, __, ___) =>
                {
                    certificate = new X509Certificate2(cert.RawData);
                    return true;
                }
            };

            var httpClient = new HttpClient(httpClientHandler);
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

            return (CertificateInfo)certificate ?? throw new NullReferenceException();
        }

        public async Task<CertificateInfo> GetCertificateInfo(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
