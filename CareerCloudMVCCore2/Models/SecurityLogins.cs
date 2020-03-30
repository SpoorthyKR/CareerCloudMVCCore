using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class SecurityLogins
    {
        public SecurityLogins()
        {
            ApplicantProfiles = new HashSet<ApplicantProfiles>();
            SecurityLoginsLog = new HashSet<SecurityLoginsLog>();
            SecurityLoginsRoles = new HashSet<SecurityLoginsRoles>();
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PasswordUpdateDate { get; set; }
        public DateTime? AgreementAcceptedDate { get; set; }
        public bool IsLocked { get; set; }
        public bool IsInactive { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public bool ForceChangePassword { get; set; }
        public string PrefferredLanguage { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<ApplicantProfiles> ApplicantProfiles { get; set; }
        public virtual ICollection<SecurityLoginsLog> SecurityLoginsLog { get; set; }
        public virtual ICollection<SecurityLoginsRoles> SecurityLoginsRoles { get; set; }
    }
}
