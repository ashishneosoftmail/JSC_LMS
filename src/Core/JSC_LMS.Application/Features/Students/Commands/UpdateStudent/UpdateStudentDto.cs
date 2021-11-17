using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Commands.UpdateStudent
{
   public  class UpdateStudentDto
    {
        [Required(ErrorMessage = "Id Is Required")]
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Student Name Is Required")]
        [StringLength(150, ErrorMessage = "Student Name length should not be more than 150 characters")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "UserType Is Required")]
        public string UserType { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 Is Required")]
        public string AddressLine1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 2  Is Required")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Phone Number Is Required")]
        [RegularExpression(@"[6-9]\d{9}", ErrorMessage = "Please enter correct mobile number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "Username should not be more than 100 characters")]
        [Required(ErrorMessage = "Username Is Required")]
        public string Username { get; set; }
     
        [Required(ErrorMessage = "City Is Required")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "State Is Required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Zip Is Required")]
        public int ZipId { get; set; }
        [Required(ErrorMessage = "Status Is Required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Class Is Required")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Section Is Required")]
        public int SectionId { get; set; }


    }
}
