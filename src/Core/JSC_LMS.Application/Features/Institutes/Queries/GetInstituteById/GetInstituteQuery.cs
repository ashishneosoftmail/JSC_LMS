﻿using JSC_LMS.Application.Features.Institutes.Queries.GetInstituteList;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteById
{
   
    public class GetInstituteQuery : IRequest<Response<GetInstituteListVm>>
    {
        public int Id { get; set; }
    }
}
