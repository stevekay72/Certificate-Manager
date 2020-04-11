using System.Security.Cryptography.X509Certificates;
using System;
using CertificateServices.Models;
using CertificateServices.Interfaces;

namespace CertificateServices.Services
{
    public class DNParser : IDNParser
    {
        public DNParser()
        {
        }

        public DN Parse(string dn)
        {
            X500DistinguishedName dname = new X500DistinguishedName(dn);
            var elements = dname.Format(true).Split("\n".ToCharArray(), options: StringSplitOptions.RemoveEmptyEntries);

            var newDN = new DN(dn);
            foreach (var element in elements)
            {
                var kv = element.Split('=');
                switch (kv[0].ToUpper())
                {
                    case "CN": newDN.Cn = kv[1]; break;
                    case "O": newDN.O = kv[1]; break;
                    case "C": newDN.C = kv[1]; break;
                    case "S": newDN.S = kv[1]; break;
                    case "OU": newDN.Ou.Add(kv[1]); break;
                }
                newDN.Dictionary.Add(kv[0], kv[1]);
            }
            return newDN;
        }


    }
}
