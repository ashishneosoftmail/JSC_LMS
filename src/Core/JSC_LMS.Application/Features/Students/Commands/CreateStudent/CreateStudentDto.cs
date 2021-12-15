using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentDto
    {

        [Required(ErrorMessage = "Student Name is mandatory")]
        [StringLength(150, ErrorMessage = "Student Name length should not be more than 150 characters")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "UserType is mandatory")]
        public string UserType { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 is mandatory")]
        public string AddressLine1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 2  is mandatory")]
        public string AddressLine2 { get; set; }
        
        [Required(ErrorMessage = "Phone Number is mandatory")]
        [StringLength(10, ErrorMessage = "Please enter correct mobile number")]
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string Mobile { get; set; }
      
        public string Email { get; set; }
       
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
        [Required(ErrorMessage = "Status is mandatory")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Class is mandatory")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Section is mandatory")]
        public int SectionId { get; set; }

        [Required(ErrorMessage = "School is mandatory")]
        public int SchoolId { get; set; }

    }
}
