using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Features.ManageProfile.ChangePassword;
using JSC_LMS.Application.Features.ManageProfile.UpdateProfileInfo;
using JSC_LMS.Application.Models.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace JSC_LMS.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            return Ok(await _authenticationService.RegisterAsync(request));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            return Ok(await _authenticationService.RefreshTokenAsync(request));
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenRequest request)
        {
            var response = await _authenticationService.RevokeToken(request);
            if (response.Message == "Token is required")
                return BadRequest(response);
            else if (response.Message == "Token did not match any users")
                return NotFound(response);
            else
                return Ok(response);
        }


        [HttpPost("TemporaryPasswordValidateEmail")]
        public async Task<ActionResult<TemporaryPasswordEmailValidateResponse>> TemporaryPasswordEmailValidate(string email)
        {
            return Ok(await _authenticationService.TempPasswordValidateEmail(email));
        }
        [HttpPost("VerfiyTemporaryPassword")]
        public async Task<ActionResult<VerifyTemporaryPasswordResponse>> VerfiyTemporaryPassword(VerfiyTemporaryPasswordRequest verfiyTemporaryPasswordRequest)
        {
            return Ok(await _authenticationService.VerifyTemporaryPassword(verfiyTemporaryPasswordRequest));

        }
        [HttpPut("UpdateForgotPasswordToNewPassword")]
        public async Task<ActionResult<UpdateResetPasswordResponse>> UpdateForgotPasswordToNewPassword(UpdateResetPasswordRequest UpdateResetPasswordRequest)
        {
            return Ok(await _authenticationService.UpdateForgotPasswordToNewPassword(UpdateResetPasswordRequest));
        }

        [HttpPut("UpdateChangePassword", Name = "UpdateChangePassword")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateChangePassword(ChangePasswordDto changePasswordDto)
        {
            var updateChangePasswordCommand = new ChangePasswordCommand(changePasswordDto);
            var response = await _mediator.Send(updateChangePasswordCommand);
            return Ok(response);
        }


        [HttpPut("UpdateProfileInformation", Name = "UpdateProfileInformation")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateProfileInformation(UpdateProfileInfoDto updateProfileInformationDto)
        {
            var updateProfileCommand = new UpdateProfileCommand(updateProfileInformationDto);
            var response = await _mediator.Send(updateProfileCommand);
            return Ok(response);
        }

    }
}
