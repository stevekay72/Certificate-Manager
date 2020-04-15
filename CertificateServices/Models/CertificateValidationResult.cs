using System;
using System.Collections.Generic;
using System.Text;

namespace CertificateServices.Models
{
    public class StatusInfo
    {
        public string Status { get; set; }
        public string StatusDescription { get; set; }
    }

    public class CertificateValidationResult
    {
        public IEnumerable<StatusInfo> Status { get; set; }
        public bool IsValid { get; set; }
    }
}
