using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class TeacherDetailsViewModel
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Class{ get; set; }
        public string Section { get; set; }
        public string Subject { get; set; }
        public string School{ get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string State{ get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }

    }
}
