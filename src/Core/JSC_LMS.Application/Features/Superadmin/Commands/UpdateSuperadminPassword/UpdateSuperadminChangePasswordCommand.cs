using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword
{
    public class UpdateSuperadminChangePasswordCommand : IRequest<Response<string>>
    {
        internal object updateSuperadminDto;

        public UpdateSuperadminChangePasswordCommand(UpdateSuperadminChangePasswordDto _updateSuperadminChangePasswordDto)
        {
            updateSuperadminChangePasswordDto = _updateSuperadminChangePasswordDto;
        }
        public UpdateSuperadminChangePasswordDto updateSuperadminChangePasswordDto { get; set; }
    }
}
