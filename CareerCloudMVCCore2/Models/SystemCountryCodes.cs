using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class SystemCountryCodes
    {
        public SystemCountryCodes()
        {
            ApplicantProfiles = new HashSet<ApplicantProfiles>();
            ApplicantWorkHistory = new HashSet<ApplicantWorkHistory>();
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApplicantProfiles> ApplicantProfiles { get; set; }
        public virtual ICollection<ApplicantWorkHistory> ApplicantWorkHistory { get; set; }
    }
}
