using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Queries.GetGallaryById
{
   public class GetGallaryByIdQuery : IRequest<Response<GetGallaryByIdDto>>
    {
        public int Id { get; set; }
    }
}
