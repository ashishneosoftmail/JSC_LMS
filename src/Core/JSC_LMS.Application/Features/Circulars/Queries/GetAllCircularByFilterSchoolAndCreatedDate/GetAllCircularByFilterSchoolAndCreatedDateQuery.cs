using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilterSchoolAndCreatedDate
{

    public class GetAllCircularByFilterSchoolAndCreatedDateQuery : IRequest<Response<IEnumerable<GetAllCircularByFilterSchoolAndCreatedDateDto>>>
    {
        public string CircularTitle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SchoolId { get; set; }
        public GetAllCircularByFilterSchoolAndCreatedDateQuery(string _CircularTitle, string _Description, DateTime _CreatedDate, int schoolid)
        {
            CircularTitle = _CircularTitle;
            Description = _Description;
            CreatedDate = _CreatedDate;
            SchoolId = schoolid;
        }

    }
}
