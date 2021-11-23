using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectList
{
   public class GetSubjectListQuery : IRequest<Response<IEnumerable<GetSubjectListDto>>>
    {
      
    }
}
