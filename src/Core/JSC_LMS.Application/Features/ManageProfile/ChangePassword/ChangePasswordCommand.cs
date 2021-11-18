using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ManageProfile.ChangePassword
{
 
        public class ChangePasswordCommand : IRequest<Response<string>>
        {
            public ChangePasswordDto changePasswordDto { get; set; }

            public ChangePasswordCommand(ChangePasswordDto _changePasswordDto)
            {
                changePasswordDto = _changePasswordDto;
            }
        }
    }

