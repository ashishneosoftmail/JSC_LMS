using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminChangePassword
{

    public class UpdateInstituteAdminChangePasswordCommandHandler : IRequestHandler<UpdateInstituteAdminChangePasswordCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UpdateInstituteAdminChangePasswordCommandHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }
        public async Task<Response<string>> Handle(UpdateInstituteAdminChangePasswordCommand request, CancellationToken cancellationToken)
        {

            Response<string> UpdateSuperadminCommandResponse = new Response<string>();

            var user = await _authenticationService.ChangeUserPassword(request.updateInstituteAdminChangePasswordDto.UserId, request.updateInstituteAdminChangePasswordDto.CurrentPassword, request.updateInstituteAdminChangePasswordDto.NewPassword);
            if (!user.Succeeded)
            {
                UpdateSuperadminCommandResponse.Message = "Password Not Changed Successfully";
                return UpdateSuperadminCommandResponse;
            }
            return new Response<string>(request.updateInstituteAdminChangePasswordDto.UserId, user.Message);
        }
    }
}
