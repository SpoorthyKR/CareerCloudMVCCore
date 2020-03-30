using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class SystemLanguageCodes
    {
        public SystemLanguageCodes()
        {
            CompanyDescriptions = new HashSet<CompanyDescriptions>();
        }

        public string LanguageId { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }

        public virtual ICollection<CompanyDescriptions> CompanyDescriptions { get; set; }
    }
}
