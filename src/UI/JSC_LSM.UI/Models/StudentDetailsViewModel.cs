using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class StudentDetailsViewModel
    {

        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string StudentName { get; set; }
        public string UserType { get; set; }
        public string Mobile { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
        public string SectionName { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get;  set; }
    }
}
