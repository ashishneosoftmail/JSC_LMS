using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsByFilter
{
    public class GetParentsByFilterQuery : IRequest<Response<IEnumerable<GetParentsByFilterDto>>>
    {
        public GetParentsByFilterQuery(string _ClassName, string _SectionName, string _StudentName,string _ParentName, bool _IsActive, DateTime _CreatedDate)
        {
            ClassName = _ClassName;
            SectionName = _SectionName;
            StudentName = _StudentName;
            ParentName=_ParentName;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }
        public string ClassName { get; set; }
        public string StudentName { get; set; }
        public string ParentName { get; set; }
        public string SectionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
