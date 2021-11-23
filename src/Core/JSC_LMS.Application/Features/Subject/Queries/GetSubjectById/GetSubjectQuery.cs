using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Queries.GetSubjectById
{
  public  class GetSubjectQuery : IRequest<Response<GetSubjectByIdVm>>
    {
        public int Id { get; set; }
    }
}
