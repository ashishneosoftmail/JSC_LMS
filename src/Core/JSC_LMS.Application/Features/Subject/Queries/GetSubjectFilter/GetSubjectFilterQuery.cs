using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectFilter
{
    public class GetSubjectFilterQuery : IRequest<Response<IEnumerable<GetSubjectFilterDto>>>
    {

        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public GetSubjectFilterQuery(string _SubjectName, string _SchoolName, string _ClassName, string _SectionName, bool _IsActive, DateTime _CreatedDate)
        {
            SchoolName = _SchoolName;
            ClassName = _ClassName;
            SectionName = _SectionName;
            SubjectName = _SubjectName;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }
    }
}
