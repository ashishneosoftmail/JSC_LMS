using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Commands.UpdateSchool
{
    public class UpdateSchoolDto
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string SchoolName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int ZipId { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
