using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class SecurityLoginsLog
    {
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        public string SourceIp { get; set; }
        public DateTime LogonDate { get; set; }
        public bool IsSuccesful { get; set; }

        public virtual SecurityLogins LoginNavigation { get; set; }
    }
}
