using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionFilter
{
   public class GetSectionFilterQuery : IRequest<Response<IEnumerable<GetSectionFilterDto>>>
    {
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
   
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public GetSectionFilterQuery(string _SchoolName, string _ClassName, string _SectionName, bool _IsActive, DateTime _CreatedDate)
        {
            SchoolName = _SchoolName;
            ClassName = _ClassName;
            SectionName = _SectionName;
           
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }

    }
}
