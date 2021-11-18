using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.ManageProfile.ChangePassword
{

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IAuthenticationService _authenticationService;
            public ChangePasswordCommandHandler(IMapper mapper, IAuthenticationService authenticationService)
            {
                _mapper = mapper;
                _authenticationService = authenticationService;
            }
            public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {

                Response<string> ChangePasswordCommandResponse = new Response<string>();

                var user = await _authenticationService.ChangeUserPassword(request.changePasswordDto.UserId, request.changePasswordDto.CurrentPassword, request.changePasswordDto.NewPassword);
                if (!user.Succeeded)
                {
                    ChangePasswordCommandResponse.Message = "Password Not Changed Successfully";
                    return ChangePasswordCommandResponse;
                }
                return new Response<string>(request.changePasswordDto.UserId, user.Message);
            }
        }
    }

