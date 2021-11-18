using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.ManageProfile.UpdateProfileInfo
{
   public class UpdateProfileCommand : IRequest<Response<int>>
    {
        public UpdateProfileCommand(UpdateProfileInfoDto _updateProfileInfoDto)
        {
            updateProfileInfoDto = _updateProfileInfoDto;
        }
        public UpdateProfileInfoDto updateProfileInfoDto { get; set; }
    }
}

