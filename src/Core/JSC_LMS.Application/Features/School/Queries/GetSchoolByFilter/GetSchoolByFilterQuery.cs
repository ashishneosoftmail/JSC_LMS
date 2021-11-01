using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolByFilter
{
   public  class GetSchoolByFilterQuery : IRequest<Response<IEnumerable<GetSchoolByFilterDto>>>
    {
        public GetSchoolByFilterQuery(string _SchoolName, string _InstituteName, bool _IsActive)
        {
            SchoolName = _SchoolName;
            InstituteName = _InstituteName;
            IsActive = _IsActive;
        }
        public string SchoolName { get; set; }
        public string InstituteName { get; }
        public bool IsActive { get; set; }
    }
}
