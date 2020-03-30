using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class CompanyDescriptions
    {
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        public string LanguageId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfiles CompanyNavigation { get; set; }
        public virtual SystemLanguageCodes Language { get; set; }
    }
}
