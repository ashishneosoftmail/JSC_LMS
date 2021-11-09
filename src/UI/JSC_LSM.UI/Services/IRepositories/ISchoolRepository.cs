using JSC_LMS.Application.Features.Class.Commands.CreateClass;
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
        #region-Developed By Harsh Chheda
        /// <summary>
        /// Get All School  - Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        Task<GetAllSchoolResponseModel> GetAllSchool();
        #endregion

    }
}
