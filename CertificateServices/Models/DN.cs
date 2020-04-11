using System;
using System.Collections;
using System.Collections.Generic;

namespace CertificateServices.Models
{
    public class DN
    {
        public DN() { }
        public DN(string rawData)
        {
            RawData = rawData;
            Ou = new List<string>();
            Dictionary = new Dictionary<string, string>();
        }
        public string RawData { get; }
        public string Cn { get; set; }
        public string O { get; set; }
        public string C { get; set; }
        public string S { get; set; }
        public IList<string> Ou { get; set; }
        public IDictionary<string, string> Dictionary { get; }
    }
}
