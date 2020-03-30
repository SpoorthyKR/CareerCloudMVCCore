using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class CompanyJobsDescriptions
    {
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string JobName { get; set; }
        public string JobDescriptions { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual CompanyJobs JobNavigation { get; set; }
    }
}
