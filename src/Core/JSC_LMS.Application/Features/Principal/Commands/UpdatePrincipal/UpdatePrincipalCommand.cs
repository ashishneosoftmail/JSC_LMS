﻿using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal
{
    public class UpdatePrincipalCommand : IRequest<Response<int>>
    {
        public UpdatePrincipalDto updatePrincipalDto { get; set; }
    }
}