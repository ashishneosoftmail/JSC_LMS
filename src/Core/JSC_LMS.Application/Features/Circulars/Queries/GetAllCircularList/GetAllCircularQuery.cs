using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetAllCircularList
{
    public class GetAllCircularQuery : IRequest<Response<IEnumerable<GetAllCircularListDto>>>
    {
    }
}
