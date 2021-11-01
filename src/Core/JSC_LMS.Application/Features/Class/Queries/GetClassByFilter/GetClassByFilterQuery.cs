using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassByFilter
{
   public class GetClassByFilterQuery:IRequest<Response<IEnumerable<SchoolFilterDtoVms>>>
    {
        public GetClassByFilterQuery(string _SchoolName, string _ClasslName, bool _IsActive, DateTime _CreatedDate)
        {
            SchoolName = _SchoolName;
            ClassName = _ClasslName;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
