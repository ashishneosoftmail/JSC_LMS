using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.School.Commands.UpdateSchool
{
    public class UpdateSchoolDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Institute is mandatory")]
        public int InstituteId { get; set; }
        [Required(ErrorMessage = "SchoolName is mandatory")]
        public string SchoolName { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 is mandatory")]
        public string Address1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 2  is mandatory")]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "ContactPerson is mandatory")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "City is mandatory")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "State is mandatory")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Zip is mandatory")]
        public int ZipId { get; set; }
        [Required(ErrorMessage = "Phone Number is mandatory")]
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
