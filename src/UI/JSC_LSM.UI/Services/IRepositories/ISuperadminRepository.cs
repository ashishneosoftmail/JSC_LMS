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
        Task<UpdateSuperadminProfileInformationResponseModel> UpdateSuperadminPersonalInformation(UpdateSuperadminDto updateSuperadminDto);
        Task<GetSuperadminByUserIdResponseModel> GetSuperadminByUserId(string id);
        Task<SuperadminChangePasswordResponseModel> SuperAdminChangePassword(UpdateSuperadminChangePasswordDto updateSuperadminChangePasswordDto);
        Task<UpdateSuperadminImageResponseModel> UpdateSuperadminImage(int Id, string LogoImageFileName, string LoginImageFileName);
    }
}
