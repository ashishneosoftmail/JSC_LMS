using JSC_LMS.Application.CommonDtos;
using JSC_LMS.Application.Features.School.Queries.GetSchoolById;
using System;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolList
{
    public class GetSchoolListDto
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string SchoolName { get; set; }
        public string Mobile { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }

        public bool IsActive { get; set; }
        public CityDto City { get; set; }
        public StateDto State { get; set; }
        public ZipDto Zip { get; set; }
        public InstituteDto Institute { get; set; }
    }
}