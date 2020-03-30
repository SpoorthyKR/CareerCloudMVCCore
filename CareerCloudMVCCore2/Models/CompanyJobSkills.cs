using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class CompanyJobSkills
    {
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string Skill { get; set; }
        public string SkillLevel { get; set; }
        public int Importance { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual CompanyJobs JobNavigation { get; set; }
    }
}
