using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Queries.GetAcademicById
{
   public class GetAcademicByIdQuery : IRequest<Response<GetAcademicByIdDto>>
    {
        public int Id { get; set; }
    }
}
