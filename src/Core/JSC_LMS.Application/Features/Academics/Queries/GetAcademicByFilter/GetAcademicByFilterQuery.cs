using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicByFilter
{
  public  class GetAcademicByFilterQuery : IRequest<Response<IEnumerable<GetAcademicByFilterDto>>>
    {
        public GetAcademicByFilterQuery(string _ClassName, string _SchoolName, string _SubjectName, string _SectionName, string _TeacherName, string _Type, bool _IsActive, DateTime _CreatedDate)
        {
            ClassName = _ClassName;
            SchoolName = _SchoolName;
            SubjectName = _SubjectName;
            TeacherName = _TeacherName;
            SectionName = _SectionName;
            Type = _Type;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string SchoolName { get; set; }
        public string TeacherName { get; set; }
        public string SectionName { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
