using JSC_LMS.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherFilter
{
    public class GetTeacherByFilterDto
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string UserType { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CityDto City { get; set; }
        public StateDto State { get; set; }
        public ZipDto Zip { get; set; }
        public DateTime CreatedDate { get; set; }        
        public SubjectDto SubjectId { get; set; }
        public SchoolDto SchoolId { get; set; }
        public ClassDto ClassId { get; set; }
        // public int SectionId { get; set; }
        public SectionDto SectionId { get;  set; }
        public bool IsActive { get; set; }
    }
}
