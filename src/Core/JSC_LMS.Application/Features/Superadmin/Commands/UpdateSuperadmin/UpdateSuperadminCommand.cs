using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin
{
    public class UpdateSuperadminCommand : IRequest<Response<int>>
    {
        public UpdateSuperadminCommand(UpdateSuperadminDto _updateSuperadminDto)
        {
            updateSuperadminDto = _updateSuperadminDto;
        }
        public UpdateSuperadminDto updateSuperadminDto { get; set; }
    }
}
