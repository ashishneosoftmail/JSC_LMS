using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Students.Queries.GetStudentByFilter
{

    public class GetStudentByFilterQuery : IRequest<Response<IEnumerable<GetStudentByFilterDto>>>
    {
        public GetStudentByFilterQuery(string _ClassName, string _SectionName,string _StudentName, bool _IsActive, DateTime _CreatedDate)
        {
            ClassName = _ClassName;
            SectionName = _SectionName;
            StudentName = _StudentName;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }
        public string ClassName { get; set; }
        public string StudentName { get; set; }
        public string SectionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
