using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ParentsFeature.Queries.GetParentsById
{
    public class GetParentsByIdQuery : IRequest<Response<GetParentsByIdDto>>
    {
        public int Id { get; set; }
       
    }
}
