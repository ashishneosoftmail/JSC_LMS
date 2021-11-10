using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface ISuperadminRepository
    {
        Task<UpdateSuperadminProfileInformationResponseModel> UpdateSuperadminPersonalInformation(UpdateSuperadminDto updateSuperadminDto);
        Task<GetSuperadminByUserIdResponseModel> GetSuperadminByUserId(string id);
    }
}
