using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolByPagination
{
   public class GetSchoolByPaginationQuery:IRequest<Response<GetSchoolByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
