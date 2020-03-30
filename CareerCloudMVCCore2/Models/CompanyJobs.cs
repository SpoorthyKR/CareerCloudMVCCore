using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class CompanyJobs
    {
        public CompanyJobs()
        {
            ApplicantJobApplications = new HashSet<ApplicantJobApplications>();
            CompanyJobEducations = new HashSet<CompanyJobEducations>();
            CompanyJobSkills = new HashSet<CompanyJobSkills>();
            CompanyJobsDescriptions = new HashSet<CompanyJobsDescriptions>();
        }

        public Guid Id { get; set; }
        public Guid Company { get; set; }
        public DateTime ProfileCreated { get; set; }
        public bool IsInactive { get; set; }
        public bool IsCompanyHidden { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfiles CompanyNavigation { get; set; }
        public virtual ICollection<ApplicantJobApplications> ApplicantJobApplications { get; set; }
        public virtual ICollection<CompanyJobEducations> CompanyJobEducations { get; set; }
        public virtual ICollection<CompanyJobSkills> CompanyJobSkills { get; set; }
        public virtual ICollection<CompanyJobsDescriptions> CompanyJobsDescriptions { get; set; }
    }
}
