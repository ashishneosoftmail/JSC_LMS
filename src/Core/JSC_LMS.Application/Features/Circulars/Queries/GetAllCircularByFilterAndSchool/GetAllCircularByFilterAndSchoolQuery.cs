using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilterAndSchool
{

    public class GetAllCircularByFilterAndSchoolQuery : IRequest<Response<IEnumerable<GetAllCircularByFilterAndSchoolDto>>>
    {
        public string CircularTitle { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int SchoolId { get; set; }
        public GetAllCircularByFilterAndSchoolQuery(string _CircularTitle, string _Description, bool _Status, int schoolid)
        {
            CircularTitle = _CircularTitle;
            Description = _Description;
            Status = _Status;
            SchoolId = schoolid;
        }

    }
}
