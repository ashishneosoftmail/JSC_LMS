using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute
{
    public class CreateInstituteDto
    {       
        [Required(ErrorMessage = "Institute Name Is Required")]
        [StringLength(150,ErrorMessage = "Institute Name length should not be more than 150 characters")]
        public string InstituteName { get; set; }
        [StringLength(100,ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 Is Required")]
        public string AddressLine1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line  Is Required")]
        public string AddressLine2 { get; set; }
        [StringLength(150, ErrorMessage = "Contact Person should not be more than 150 characters")]
        [Required(ErrorMessage = "Contact Person Is Required")]
        public string ContactPerson { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string Mobile { get; set; }
        [StringLength(100, ErrorMessage = "Email should not be more than 100 characters")]
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "Username should not be more than 100 characters")]
        [Required(ErrorMessage = "Username Is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Password should contain 8 character - one uppercase, one lowercase, one numeric value and a special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "City Is Required")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "State Is Required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Zip Is Required")]
        public int ZipId { get; set; }
        [Required(ErrorMessage = "License Expiry date Is Required")]
        public DateTime LicenseExpiry { get; set; }
        [StringLength(150, ErrorMessage = "Institute URL should not be more than 150 characters")]
        [Required(ErrorMessage = "Institute URL Is Required")]
        public string InstituteURL { get; set; }
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Status Is Required")]
        public bool IsActive { get; set; }

    }
}
