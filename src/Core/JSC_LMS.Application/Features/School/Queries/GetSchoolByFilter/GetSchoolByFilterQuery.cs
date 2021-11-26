using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolByFilter
{
   public class GetSchoolByFilterQuery:IRequest<Response<IEnumerable<GetSchoolByFilterDto>>>
    {
        public GetSchoolByFilterQuery(string _SchoolName, string _City, string _State, string _InstituteName, bool _IsActive, DateTime _CreatedDate)
        {
            SchoolName = _SchoolName;
            InstituteName = _InstituteName;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
            Cityname = _City;
            Statename = _State;

        }
        public string SchoolName { get; set; }
        public string InstituteName { get; set; }
        public string Cityname { get; set; }
        public string Statename { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
