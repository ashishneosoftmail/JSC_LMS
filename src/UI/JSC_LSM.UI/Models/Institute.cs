using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class Institute
    {
        [DisplayName("Institute Name")]
        [Required(ErrorMessage = "Institute Name Is Required")]
        public string InstituteName { get; set; }
        [DisplayName("Address 1")]
        [Required(ErrorMessage = "Address Line 1 Is Required")]
        public string AddressLine1 { get; set; }
        [DisplayName("Address 2")]
        [Required(ErrorMessage = "Address Line 2 Is Required")]
        public string AddressLine2 { get; set; }
        [DisplayName("Contact Person")]
        [Required(ErrorMessage = "Contact Person Is Required")]
        public string ContactPerson { get; set; }
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string Mobile { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage = "Username Is Required")]
        public string Username { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]

        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Password should contain 8 character - one uppercase, one lowercase, one numeric value and a special character.")]
        public string Password { get; set; }
        [DisplayName("City")]
        [Required(ErrorMessage = "City Is Required")]
        public int City { get; set; }
        [DisplayName("State")]
        [Required(ErrorMessage = "State Is Required")]
        public int State { get; set; }
        [DisplayName("Zipcode")]
        [Required(ErrorMessage = "Zip Code Is Required")]
        public int Zip { get; set; }
        [DisplayName("License Expiry")]
        [Required(ErrorMessage = "License Expiry date Is Required")]
        public DateTime LicenseExpiry { get; set; }
        [DisplayName("Institute URL")]
        [Required(ErrorMessage = "Institute URL Is Required")]
        public string InstituteURL { get; set; }
        [DisplayName("Status")]
        [Required(ErrorMessage = "Status Is Required")]
        public bool IsActive { get; set; }

        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> ZipCode { get; set; }

    }
}
