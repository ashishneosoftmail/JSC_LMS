using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword
{
    public class UpdateSuperadminChangePasswordCommandHandler : IRequestHandler<UpdateSuperadminChangePasswordCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UpdateSuperadminChangePasswordCommandHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }
        public async Task<Response<string>> Handle(UpdateSuperadminChangePasswordCommand request, CancellationToken cancellationToken)
        {

            Response<string> UpdateSuperadminCommandResponse = new Response<string>();

            var user = await _authenticationService.ChangeUserPassword(request.updateSuperadminChangePasswordDto.User, request.updateSuperadminChangePasswordDto.UserId, request.updateSuperadminChangePasswordDto.CurrentPassword, request.updateSuperadminChangePasswordDto.NewPassword);
            if (!user.Succeeded)
            {
                UpdateSuperadminCommandResponse.Message = "Password Not Changed Successfully";
                return UpdateSuperadminCommandResponse;
            }
            return new Response<string>(request.updateSuperadminChangePasswordDto.UserId, user.Message);
        }
    }
}
