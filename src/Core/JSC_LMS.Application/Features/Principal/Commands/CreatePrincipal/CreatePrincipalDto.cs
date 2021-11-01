﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal
{
    public class CreatePrincipalDto
    {
        [Required(ErrorMessage = "School Is Required")]
        public int SchoolId { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 1 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line 1 Is Required")]
        public string AddressLine1 { get; set; }
        [StringLength(100, ErrorMessage = "Address Line 2 should not be more than 100 characters")]
        [Required(ErrorMessage = "Address Line  Is Required")]
        public string AddressLine2 { get; set; }
        [StringLength(150, ErrorMessage = "Name should not be more than 150 characters")]
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string Mobile { get; set; }
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
        [Required(ErrorMessage = "Status Is Required")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Role Is Required")]
        public string RoleName { get; set; }
    }
}
