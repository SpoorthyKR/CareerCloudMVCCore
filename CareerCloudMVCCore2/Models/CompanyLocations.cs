using System;
using System.Collections.Generic;

namespace CareerCloudMVCCore2.Models
{
    public partial class CompanyLocations
    {
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        public string CountryCode { get; set; }
        public string StateProvinceCode { get; set; }
        public string StreetAddress { get; set; }
        public string CityTown { get; set; }
        public string ZipPostalCode { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfiles CompanyNavigation { get; set; }
    }
}
