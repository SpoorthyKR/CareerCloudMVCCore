using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class ApplicantEducations
    {
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public string Major { get; set; }
        public string CertificateDiploma { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public byte? CompletionPercent { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ApplicantProfiles ApplicantNavigation { get; set; }
    }
}
