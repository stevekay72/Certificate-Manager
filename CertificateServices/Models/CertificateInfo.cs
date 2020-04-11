using System;
using System.Security.Cryptography.X509Certificates;

namespace CertificateServices.Models
{
    public class CertificateInfo
    {
        public Uri Url { get; set; }
        public string Title { get; set; }
        public string KeyHash { get; set; }
        public DateTime Expiry { get; set; }
        public string Issuer { get; set; }

        public CertificateInfo() { }

        public CertificateInfo(X509Certificate2 certificate)
        {
            if (certificate != null)
            {
                this.Issuer = certificate.Issuer;
                this.KeyHash = certificate.GetCertHashString();
                this.Expiry = DateTime.Parse(certificate.GetExpirationDateString());
            }
        }

        public static explicit operator CertificateInfo(X509Certificate2 certificate)
        {
            return new CertificateInfo(certificate);
        }
    }
}
