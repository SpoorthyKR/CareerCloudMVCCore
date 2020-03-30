using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class SecurityRoles
    {
        public SecurityRoles()
        {
            SecurityLoginsRoles = new HashSet<SecurityLoginsRoles>();
        }

        public Guid Id { get; set; }
        public string Role { get; set; }
        public bool IsInactive { get; set; }

        public virtual ICollection<SecurityLoginsRoles> SecurityLoginsRoles { get; set; }
    }
}
