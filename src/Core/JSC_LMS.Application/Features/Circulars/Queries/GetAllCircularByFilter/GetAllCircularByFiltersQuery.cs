using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularByFilter
{
    public class GetAllCircularByFiltersQuery : IRequest<Response<IEnumerable<GetAllCircularByFiltersDto>>>
    {
        public string CircularTitle { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public GetAllCircularByFiltersQuery(string _CircularTitle, string _Description, bool _Status)
        {
            CircularTitle = _CircularTitle;
            Description = _Description;
            Status = _Status;
        }
    }
}
