using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Queries.GetTeacherById
{
    public class GetTeacherQuery : IRequest<Response<GetTeacherByIdVm>>
    {
        public int Id { get; set; }
    }

}
