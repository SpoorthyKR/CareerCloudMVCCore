using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class CompanyProfiles
    {
        public CompanyProfiles()
        {
            CompanyDescriptions = new HashSet<CompanyDescriptions>();
            CompanyJobs = new HashSet<CompanyJobs>();
            CompanyLocations = new HashSet<CompanyLocations>();
        }

        public Guid Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string CompanyWebsite { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }
        public byte[] CompanyLogo { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<CompanyDescriptions> CompanyDescriptions { get; set; }
        public virtual ICollection<CompanyJobs> CompanyJobs { get; set; }
        public virtual ICollection<CompanyLocations> CompanyLocations { get; set; }
    }
}
