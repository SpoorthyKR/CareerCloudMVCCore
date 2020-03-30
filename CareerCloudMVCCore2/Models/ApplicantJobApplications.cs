using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class ApplicantJobApplications
    {
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public Guid Job { get; set; }
        public DateTime ApplicationDate { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ApplicantProfiles ApplicantNavigation { get; set; }
        public virtual CompanyJobs JobNavigation { get; set; }
    }
}
