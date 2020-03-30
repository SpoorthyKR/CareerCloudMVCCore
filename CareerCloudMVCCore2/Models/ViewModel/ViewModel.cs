using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloudMVCCore2.Models.ViewModel
{
    public class ViewModel
    {
        public IEnumerable<ApplicantEducations> ApplicantEducations { get; set; }
        public IEnumerable<ApplicantJobApplications> ApplicantJobApplications { get; set; }
        public IEnumerable<ApplicantProfiles> ApplicantProfiles { get; set; }
        public IEnumerable<ApplicantResumes> ApplicantResumes { get; set; }
        public IEnumerable<ApplicantSkills> ApplicantSkills { get; set; }
        public IEnumerable<ApplicantWorkHistory> ApplicantWorkHistory { get; set; }
        public IEnumerable<CompanyDescriptions> CompanyDescriptions { get; set; }
        public IEnumerable<CompanyJobEducations> CompanyJobEducations { get; set; }
        public IEnumerable<CompanyJobs> CompanyJobs { get; set; }
        public IEnumerable<CompanyJobsDescriptions> CompanyJobsDescriptions { get; set; }
        public IEnumerable<CompanyJobSkills> CompanyJobSkills { get; set; }
        public IEnumerable<CompanyLocations> CompanyLocations { get; set; }
        public IEnumerable<CompanyProfiles> CompanyProfiles { get; set; }
        public IEnumerable<SecurityLogins> SecurityLogins { get; set; }
        public IEnumerable<SecurityLoginsLog> SecurityLoginsLog { get; set; }
        public IEnumerable<SecurityLoginsRoles> SecurityLoginsRoles { get; set; }
        public IEnumerable<SecurityRoles> SecurityRoles { get; set; }
        public IEnumerable<SystemCountryCodes> SystemCountryCodes { get; set; }
        public IEnumerable<SystemLanguageCodes> SystemLanguageCodes { get; set; }
    }
}
