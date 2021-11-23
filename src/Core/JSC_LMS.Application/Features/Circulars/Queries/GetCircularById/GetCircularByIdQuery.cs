using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Circulars.Queries.GetCircularById
{
    public class GetCircularByIdQuery : IRequest<Response<GetCircularByIdDto>>
    {
        public int Id { get; set; }
    }
}
