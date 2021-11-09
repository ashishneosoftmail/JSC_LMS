using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class InstituteDetailsViewModel
    {

        public int Id { get; set; }
        public string InstituteName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string InstituteURL { get; set; }
        public bool IsActive { get; set; }


    }
}
