using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal
{
    public class CreatePrincipalDto
    {
        public int SchoolId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int ZipId { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
    }
}
