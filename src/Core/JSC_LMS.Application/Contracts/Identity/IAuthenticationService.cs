using JSC_LMS.Application.Models.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<IEnumerable<RolesResponse>> GetAllRoles();
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task<RevokeTokenResponse> RevokeToken(RevokeTokenRequest request);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);
        Task<GetUserByIdResponse> GetUserById(string id);
        Task<ChangeUserPasswordResponse> ChangeUserPassword(string userid, string oldPassword, string newPassword);
        Task<TemporaryPasswordEmailValidateResponse> TempPasswordValidateEmail(string email);
        Task<VerifyTemporaryPasswordResponse> VerifyTemporaryPassword(VerfiyTemporaryPasswordRequest verfiyTemporaryPasswordRequest);
        Task<UpdateResetPasswordResponse> UpdateForgotPasswordToNewPassword(UpdateResetPasswordRequest updateResetPasswordRequest);
    }
}
