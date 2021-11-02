using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherFilter
{
    public class GetTeacherFilterQuery : IRequest<Response<IEnumerable<GetTeacherByFilterDto>>>
    {
        public string TeacherName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public GetTeacherFilterQuery(string _TeacherName,string _SubjectName, string _ClassName, string _SectionName ,bool _IsActive, DateTime _CreatedDate)
        {
            TeacherName = _TeacherName;
            ClassName = _ClassName;
            SectionName = _SectionName;
            SubjectName = _SubjectName;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }
    }
}
