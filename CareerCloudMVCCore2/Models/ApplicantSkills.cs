using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class ApplicantSkills
    {
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public string Skill { get; set; }
        public string SkillLevel { get; set; }
        public byte StartMonth { get; set; }
        public int StartYear { get; set; }
        public byte EndMonth { get; set; }
        public int EndYear { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ApplicantProfiles ApplicantNavigation { get; set; }
    }
}
