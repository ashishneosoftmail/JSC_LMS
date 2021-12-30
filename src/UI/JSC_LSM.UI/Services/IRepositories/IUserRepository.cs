using JSC_LMS.Application.Features.ManageProfile.ChangePassword;
using JSC_LMS.Application.Features.ManageProfile.UpdateProfileInfo;
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
        Task<AuthenticationResponseModel> UserAuthenticate(string baseurl, AuthenticationRequest authenticateRequest);

        
             Task<ChangePasswordResponseModel> UpdateChangePassword(string baseurl, ChangePasswordDto changePasswordDto);

        Task<UpdateProfileInformationResponseModel> UpdatePersonalInformation(string baseurl, UpdateProfileInfoDto updateProfileInformationDto);

        Task<TemporaryPasswordEmailValidateResponse> TemporaryPasswordEmailValidate(string baseurl, string email);
        Task<VerifyTemporaryPasswordResponse> VerfiyTemporaryPassword(string baseurl, VerfiyTemporaryPasswordRequest verfiyTemporaryPasswordRequest);
        Task<UpdateResetPasswordResponse> UpdateForgotPasswordToNewPassword(string baseurl, UpdateResetPasswordRequest UpdateResetPasswordRequest);
        Task<GetAllUsersResponseModel> GetAllUser(string baseurl);

    }
}
