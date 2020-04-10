using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
        static void Main(string[] args)
        {
            Console.WriteLine("Cert Checker!");
            //var url = "www.google.com";
            var url = "www.fca.org.uk";

            var cert = GetServerCertificateAsync($"https://{url}").Result;
            var expiry = cert.GetExpirationDateString();
            Console.WriteLine($"Expiry = {expiry}");

            Console.ReadKey();
        }
    }
}
