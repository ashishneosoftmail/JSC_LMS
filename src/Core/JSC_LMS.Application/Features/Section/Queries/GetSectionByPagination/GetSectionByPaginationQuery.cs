using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Queries.GetSectionByPagination
{
    public class GetSectionByPaginationQuery : IRequest<Response<GetSectionListByPaginationResponse>>
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
