using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Queries.GetPrincipalByFilter
{
    public class GetPrincipalByFilterQuery : IRequest<Response<IEnumerable<GetPrincipalByFilterDto>>>
    {
        public GetPrincipalByFilterQuery(string _SchoolName, string _PrincipalName, bool _IsActive, DateTime _CreatedDate)
        {
            SchoolName = _SchoolName;
            PrincipalName = _PrincipalName;
            IsActive = _IsActive;
            CreatedDate = _CreatedDate;
        }
        public string SchoolName { get; set; }
        public string PrincipalName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
