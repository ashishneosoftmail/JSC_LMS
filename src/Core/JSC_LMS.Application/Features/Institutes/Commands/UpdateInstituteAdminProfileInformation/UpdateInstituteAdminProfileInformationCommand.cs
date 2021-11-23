using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminProfileInformation
{
    public class UpdateInstituteAdminProfileInformationCommand : IRequest<Response<int>>
    {
        public UpdateInstituteAdminProfileInformationCommand(UpdateInstituteAdminProfileInformationDto _updateInstituteAdminProfileInformationDto)
        {
            updateInstituteAdminProfileInformationDto = _updateInstituteAdminProfileInformationDto;
        }
        public UpdateInstituteAdminProfileInformationDto updateInstituteAdminProfileInformationDto { get; set; }
    }
}
