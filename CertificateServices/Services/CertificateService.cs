using CertificateServices.Interfaces;
using CertificateServices.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CertificateServices.Services
{
    public class CertificateService : ICertificateService
    {
        private CertificateValidationResult Validate(X509Certificate2 x509Certificate)
        {
            var chain = new X509Chain
            {
                ChainPolicy =
                {
                    RevocationMode = X509RevocationMode.Online,
                    RevocationFlag = X509RevocationFlag.EntireChain,
                    UrlRetrievalTimeout = new TimeSpan(1000),
                    VerificationTime = DateTime.Now
                }
            };

            var validationResult = new CertificateValidationResult
            {
                IsValid = chain.Build(x509Certificate)
            };

            if (!validationResult.IsValid)
            {
                validationResult.Status = chain.ChainStatus.Select(x => new StatusInfo
                    {Status = x.Status.ToString(), StatusDescription = x.StatusInformation});
            }

            return validationResult;
        }

        public async Task<CertificateInfo> GetCertificateInfo(Uri url)
        {
            X509Certificate2 certificate = null;
            var httpClientHandler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                DefaultProxyCredentials = CredentialCache.DefaultCredentials,
                ServerCertificateCustomValidationCallback = (_, cert, __, ___) =>
                {
                    certificate = new X509Certificate2(cert.RawData);
                    return true;
                }
            };

            var httpClient = new HttpClient(httpClientHandler);
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

            if (certificate is null) throw new NullReferenceException();
            var returnCert = new CertificateInfo(certificate)
            {
                Url = url,
                CertificateValidationResult = Validate(certificate)
            };
            return returnCert;
        }

        public async Task<CertificateInfo> GetCertificateInfo(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
