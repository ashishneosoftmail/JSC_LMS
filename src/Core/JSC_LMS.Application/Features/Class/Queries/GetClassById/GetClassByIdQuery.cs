using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Queries.GetClassById
{
   public class GetClassByIdQuery : IRequest<Response<GetClassByIdDto>>
    {
        public int Id { get; set; }
    }
}
