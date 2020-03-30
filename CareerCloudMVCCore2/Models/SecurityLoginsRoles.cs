using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class SecurityLoginsRoles
    {
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        public Guid Role { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual SecurityLogins LoginNavigation { get; set; }
        public virtual SecurityRoles RoleNavigation { get; set; }
    }
}
