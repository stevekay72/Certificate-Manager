using CertificateServices.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CertificateServices.Interfaces
{
    public interface ICertificateService
    {
        Task<CertificateInfo> GetCertificateInfo(Uri url);
        Task<CertificateInfo> GetCertificateInfo(IPAddress ipAddress);
    }
}
