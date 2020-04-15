using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CertificateServices.Models;
using DataServices.Interfaces;

namespace CertificateManager.Blazor.Data
{
    public class DataStoreService
    {
        private readonly ILocalDbService LocalDb;

        public DataStoreService(ILocalDbService localDb)
        {
            LocalDb = localDb;
        }

        public IEnumerable<CertificateInfo> GetCertificateList()
        {
            return LocalDb.All<CertificateInfo>();
        }

        public void AddCertificate(CertificateInfo certificate)
        {
            LocalDb.Upsert<CertificateInfo>(certificate);
        }
        public void RemoveCertificate(CertificateInfo certificate)
        {
            LocalDb.Delete<CertificateInfo>(certificate.Id);
        }

    }
}
