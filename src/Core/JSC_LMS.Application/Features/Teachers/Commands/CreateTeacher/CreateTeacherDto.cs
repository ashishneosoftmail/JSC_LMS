﻿using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherDto
    {
        [Required(ErrorMessage = "Teacher Name Is Required")]
        [StringLength(150, ErrorMessage = "Teacher Name length should not be more than 150 characters")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "UserType Is Required")]
        public string UserType { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 Is Required")]
        public string AddressLine1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 2 Is Required")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Class Is Required")]
        public int ClassId { get; set; }
        [StringLength(10)]
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
        [Required(ErrorMessage = "Section Is Required")]
        public int SectionId { get; set; }
        [Required(ErrorMessage = "School Is Required")]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Subject Is Required")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Status Is Required")]
        public bool IsActive { get; set; }
    }
}
