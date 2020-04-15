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
        //static async Task<X509Certificate2> GetServerCertificateAsync(string url)
        //{
        //    X509Certificate2 certificate = null;
        //    var httpClientHandler = new HttpClientHandler
        //    {
        //        UseDefaultCredentials = true,
        //        ServerCertificateCustomValidationCallback = (_, cert, __, ___) =>
        //        {
        //            certificate = new X509Certificate2(cert.RawData);
        //            return true;
        //        }
        //    };

        //    var httpClient = new HttpClient(httpClientHandler);
        //    await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

        //    return certificate ?? throw new NullReferenceException();
        //}

        private static ICertificateService _certService;
        private static IDNParser _dnParser;

        static void Main(string[] args)
        {
            Console.WriteLine("Cert Checker!");
            _certService = new CertificateService();
            _dnParser = new DNParser();

            //var url = "www.google.com";
            //var url = "www.fca.org.uk";
//            var url = "revoked.badssl.com";
            var url = "untrusted-root.badssl.com";

            var certificate = _certService.GetCertificateInfo(new Uri($"https://{url}")).Result;
            Console.WriteLine($"Expiry = {certificate.Expiry.ToString("dd MMM yyyy")}");
            Console.WriteLine($"Issuer = {_dnParser.Parse(certificate.IssuerDN).O}");
            Console.WriteLine($"Subject = {_dnParser.Parse(certificate.SubjectDN).O}");
            Console.WriteLine($"Url = {certificate.Url.DnsSafeHost}");
            Console.WriteLine($"Signature = {certificate.SignatureAlgorithm.FriendlyName}");
            Console.WriteLine($"IsValid = {certificate.IsValid}");

            if (!certificate.IsValid)
            {
                Console.WriteLine($"Status:");
                foreach (var issue in certificate.CertificateValidationResult.Status)
                    Console.WriteLine($"\t{issue.Status} - {issue.StatusDescription}");
            }

            Console.ReadKey();
        }
    }
}
