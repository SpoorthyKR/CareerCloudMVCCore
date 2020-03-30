using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class ApplicantWorkHistory
    {
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public string CompanyName { get; set; }
        public string CountryCode { get; set; }
        public string Location { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public short StartMonth { get; set; }
        public int StartYear { get; set; }
        public short EndMonth { get; set; }
        public int EndYear { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ApplicantProfiles ApplicantNavigation { get; set; }
        public virtual SystemCountryCodes CountryCodeNavigation { get; set; }
    }
}
