using System;
using CertificateServices.Models;

namespace CertificateServices.Interfaces
{
    public interface IDNParser
    {
        DN Parse(string dn);
    }
}
