using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class CompanyJobEducations
    {
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string Major { get; set; }
        public short Importance { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual CompanyJobs JobNavigation { get; set; }
    }
}
