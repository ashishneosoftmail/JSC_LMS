using JSC_LMS.Application.CommonDtos;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolById
{
    public class GetSchoolByIdDto
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string SchoolName { get; set; }
        public string Mobile { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public CityDto City { get; set; }
        public StateDto State { get; set; }
        public ZipDto Zip { get; set; }
        public InstituteDto Institute{ get; set; }
    }
}