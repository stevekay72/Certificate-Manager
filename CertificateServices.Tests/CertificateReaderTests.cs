using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using CertificateServices;
using CertificateServices.Interfaces;
using CertificateServices.Services;

namespace CertificateServices.Tests
{
    public class CertificateReaderTests
    {
        private ICertificateReader CertificateReader;
        [SetUp]
        public void Setup()
        {
            CertificateReader = new CertificateReader();
        }

        [Test]
        public async Task GetCertificateUrl()
        {
            var result = await CertificateReader.GetCertificateInfo(new Uri("https://www.google.com"));
            Assert.AreEqual("CN=GTS CA 1O1, O=Google Trust Services, C=US", result.IssuerDN);
        }

        [Test]
        public async Task GetCertificateIp()
        {
            var result = await CertificateReader.GetCertificateInfo(IPAddress.Parse("8.8.4.4"));
            Assert.AreEqual("Google", result.IssuerDN);
        }

    }
}