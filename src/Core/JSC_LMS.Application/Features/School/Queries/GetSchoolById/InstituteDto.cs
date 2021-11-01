using JSC_LMS.Application.Features.School.Queries.GetSchoolByFilter;
using System;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolById
{
    public class InstituteDto
    {
        public int Id { get; set; }
        public string InstituteName { get; set; }

        public static implicit operator InstituteDto(InstituteFilterDto v)
        {
            throw new NotImplementedException();
        }
    }
}