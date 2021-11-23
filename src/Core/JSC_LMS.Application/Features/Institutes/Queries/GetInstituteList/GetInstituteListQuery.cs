using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList
{
    public class GetInstituteListQuery :  IRequest<Response<IEnumerable<GetInstituteListVm>>>
    {
    }
}
