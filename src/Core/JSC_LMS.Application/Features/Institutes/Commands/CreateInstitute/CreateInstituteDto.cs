using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

#region - Data transfer object for create operation on Institute entity : by Shivani Goswami
namespace JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute
{
    public class CreateInstituteDto
    {       
        [Required(ErrorMessage = "Institute Name is mandatory")]
        [StringLength(150,ErrorMessage = "Institute Name length should not be more than 150 characters")]
        public string InstituteName { get; set; }
        [StringLength(100,ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 is mandatory")]
        public string AddressLine1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line  is mandatory")]
        public string AddressLine2 { get; set; }
        [StringLength(150, ErrorMessage = "Contact Person should not be more than 150 characters")]
        [Required(ErrorMessage = "Contact Person is mandatory")]
        public string ContactPerson { get; set; }
       
        [Required(ErrorMessage = "Phone Number is mandatory")]
        [StringLength(10, ErrorMessage = "Please enter correct mobile number")]
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string Mobile { get; set; }
        [StringLength(100, ErrorMessage = "Email should not be more than 100 characters")]
        [Required(ErrorMessage = "Email is mandatory")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "Username should not be more than 100 characters")]
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is mandatory")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$", ErrorMessage = "Password should contain 8 character - one uppercase, one lowercase, one numeric value and a special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "City is mandatory")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "State is mandatory")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Zip is mandatory")]
        public int ZipId { get; set; }
        [Required(ErrorMessage = "License Expiry date is mandatory")]
        public DateTime LicenseExpiry { get; set; }
        [StringLength(150, ErrorMessage = "Institute URL should not be more than 150 characters")]
        [Required(ErrorMessage = "Institute URL is mandatory")]
        public string InstituteURL { get; set; }
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public bool IsActive { get; set; }

    }
}
#endregion