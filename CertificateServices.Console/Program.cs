using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CertificateServices.Interfaces;
using CertificateServices.Services;

namespace GetCertInfo
{
    class Program
    {
        static async Task<X509Certificate2> GetServerCertificateAsync(string url)
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

            return certificate ?? throw new NullReferenceException();
        }

        private static ICertificateReader CertReader;

        static void Main(string[] args)
        {
            Console.WriteLine("Cert Checker!");
            CertReader = new CertificateReader();

            //var url = "www.google.com";
            var url = "www.fca.org.uk";

            var cert = CertReader.GetCertificateInfo(new Uri($"https://{url}")).Result;
            //var cert = GetServerCertificateAsync($"https://{url}").Result;
            var expiry = cert.Expiry;
            //var expiry = cert.GetExpirationDateString();
            Console.WriteLine($"Expiry = {expiry}");

            Console.ReadKey();
        }
    }
}
