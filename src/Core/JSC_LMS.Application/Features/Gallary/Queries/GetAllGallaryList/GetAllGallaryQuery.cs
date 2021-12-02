using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryList
{
    public class GetAllGallaryQuery:IRequest<Response<IEnumerable<GetAllGallaryListDto>>>
    {

    }
}
