using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class ApplicantProfiles
    {
        public ApplicantProfiles()
        {
            ApplicantEducations = new HashSet<ApplicantEducations>();
            ApplicantJobApplications = new HashSet<ApplicantJobApplications>();
            ApplicantResumes = new HashSet<ApplicantResumes>();
            ApplicantSkills = new HashSet<ApplicantSkills>();
            ApplicantWorkHistory = new HashSet<ApplicantWorkHistory>();
        }

        public Guid Id { get; set; }
        public Guid Login { get; set; }
        public decimal? CurrentSalary { get; set; }
        public decimal? CurrentRate { get; set; }
        public string Currency { get; set; }
        public string CountryCode { get; set; }
        public string StateProvinceCode { get; set; }
        public string StreetAddress { get; set; }
        public string CityTown { get; set; }
        public string ZipPostalCode { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual SystemCountryCodes CountryCodeNavigation { get; set; }
        public virtual SecurityLogins LoginNavigation { get; set; }
        public virtual ICollection<ApplicantEducations> ApplicantEducations { get; set; }
        public virtual ICollection<ApplicantJobApplications> ApplicantJobApplications { get; set; }
        public virtual ICollection<ApplicantResumes> ApplicantResumes { get; set; }
        public virtual ICollection<ApplicantSkills> ApplicantSkills { get; set; }
        public virtual ICollection<ApplicantWorkHistory> ApplicantWorkHistory { get; set; }
    }
}
