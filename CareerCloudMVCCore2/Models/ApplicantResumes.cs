using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class ApplicantResumes
    {
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public string Resume { get; set; }
        public DateTime? LastUpdated { get; set; }

        public virtual ApplicantProfiles ApplicantNavigation { get; set; }
    }
}
