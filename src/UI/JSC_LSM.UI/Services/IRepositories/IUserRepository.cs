using JSC_LMS.Application.Models.Authentication;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IUserRepository
    {
        Task<AuthenticationResponseModel> UserAuthenticate(AuthenticationRequest authenticateRequest);
        Task<TemporaryPasswordEmailValidateResponse> TemporaryPasswordEmailValidate(string email);
        Task<VerifyTemporaryPasswordResponse> VerfiyTemporaryPassword(VerfiyTemporaryPasswordRequest verfiyTemporaryPasswordRequest);
        Task<UpdateResetPasswordResponse> UpdateForgotPasswordToNewPassword(UpdateResetPasswordRequest UpdateResetPasswordRequest);
    }
}
