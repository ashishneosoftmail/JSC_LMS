using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Queries.GetSchoolById
{
  public   class GetSchoolByIdQuery : IRequest<Response<GetSchoolByIdDto>>
    {
        public int Id { get; set; }
    }
}

