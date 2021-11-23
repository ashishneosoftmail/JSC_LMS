using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminChangePassword
{
    public class UpdateInstituteAdminChangePasswordCommand : IRequest<Response<string>>
    {
        public UpdateInstituteAdminChangePasswordDto updateInstituteAdminChangePasswordDto { get; set; }

        public UpdateInstituteAdminChangePasswordCommand(UpdateInstituteAdminChangePasswordDto _updateInstituteAdminChangePasswordDto)
        {
            updateInstituteAdminChangePasswordDto = _updateInstituteAdminChangePasswordDto;
        }
    }

}
