using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace CertificateServices.Models
{
    public class SignatureAlgorithm
    {
        public string FriendlyName { get; set; }
        public string Value { get; set; }
    }

    public class CertificateInfo
    {
        public Uri Url { get; set; }
        public string Title { get; set; }
        public string KeyHash { get; set; }
        public DateTime Expiry { get; set; }
        public string IssuerDN { get; set; }
        public string SubjectDN { get; set; }
        public string Thumbprint { get; set; }
        public string SerialNumber { get; set; }
        public SignatureAlgorithm SignatureAlgorithm { get; set; }

        public CertificateInfo() { }

        public CertificateInfo(X509Certificate2 certificate)
        {
            if (certificate != null)
            {
                this.IssuerDN = certificate.Issuer;

                var xx = certificate.Issuer;
                var xy = certificate.GetNameInfo(X509NameType.SimpleName, true);

                this.SubjectDN = certificate.Subject;
                this.Thumbprint = certificate.Thumbprint;
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
