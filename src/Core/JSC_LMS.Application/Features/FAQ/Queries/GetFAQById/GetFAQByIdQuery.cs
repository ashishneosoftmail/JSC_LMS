using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FAQ.Queries.GetFAQById
{
   public class GetFAQByIdQuery : IRequest<Response<GetFAQByIdDto>>
    {
        public int Id { get; set; }
    }
}
