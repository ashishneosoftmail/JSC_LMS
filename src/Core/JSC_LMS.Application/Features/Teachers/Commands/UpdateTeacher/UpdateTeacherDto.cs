using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherDto
    {
        [Required(ErrorMessage = "Id is mandatory")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Teacher Name is mandatory")]
        [StringLength(150, ErrorMessage = "Teacher Name length should not be more than 150 characters")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "UserType is mandatory")]
        public string UserType { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 is mandatory")]
        public string AddressLine1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 2 is mandatory")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Class is mandatory")]
        public int ClassId { get; set; }
        [StringLength(10)]
        [Required(ErrorMessage = "Phone Number is mandatory")]
        public string Mobile { get; set; }
        [StringLength(100, ErrorMessage = "Email should not be more than 100 characters")]
        [Required(ErrorMessage = "Email is mandatory")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "Username should not be more than 100 characters")]
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }       
        public string UserId { get; set; }
        [Required(ErrorMessage = "School is mandatory")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "City is mandatory")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "State is mandatory")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Zipcode is mandatory")]
        public int ZipId { get; set; }
        [Required(ErrorMessage = "Section is mandatory")]
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Subject is mandatory")]

        public int SubjectId { get; set; }
        
        public bool IsActive { get; set; }
    }
}
