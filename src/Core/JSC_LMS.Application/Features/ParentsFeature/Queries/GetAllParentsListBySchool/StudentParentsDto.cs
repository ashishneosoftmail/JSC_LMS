using JSC_LMS.Application.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetAllParentsListBySchool
{
    public class StudentParentsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ParentName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CityDto City { get; set; }
        public StateDto State { get; set; }
        public ZipDto Zip { get; set; }
        public SchoolDto School { get; set; }
        public ClassDto Class { get; set; }
        public SectionDto Section { get; set; }       
        public int StudentId { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }

    }
}
