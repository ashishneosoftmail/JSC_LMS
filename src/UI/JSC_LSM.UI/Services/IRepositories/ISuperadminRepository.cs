using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin;
using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface ISuperadminRepository
    {
        Task<UpdateSuperadminProfileInformationResponseModel> UpdateSuperadminPersonalInformation(string baseurl, UpdateSuperadminDto updateSuperadminDto);
        Task<GetSuperadminByUserIdResponseModel> GetSuperadminByUserId(string baseurl, string id);
        Task<SuperadminChangePasswordResponseModel> SuperAdminChangePassword(string baseurl, UpdateSuperadminChangePasswordDto updateSuperadminChangePasswordDto);
        Task<UpdateSuperadminImageResponseModel> UpdateSuperadminImage(string baseurl, int Id, string LogoImageFileName, string LoginImageFileName);
    }
}
