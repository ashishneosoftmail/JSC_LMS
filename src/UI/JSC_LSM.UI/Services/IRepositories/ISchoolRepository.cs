using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface ISchoolRepository
    {
        Task<GetAllSchoolResponseModel> GetAllSchool();
        Task<PrincipalResponseModel> AddNewPrinicipal(CreatePrincipalDto createPrincipalDto);
    }
}
