using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteByPagination
{
   
    public class GetInstituteByPaginationQuery : IRequest<Response<GetInstituteListByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
