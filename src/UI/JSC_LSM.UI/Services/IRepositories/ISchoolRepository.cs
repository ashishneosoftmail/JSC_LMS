using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using JSC_LMS.Application.Features.School.Commands.UpdateSchool;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.ResponseModels.SchoolResponseModels;
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

        Task<SchoolResponseModel> AddNewSchool(CreateSchoolDto createSchoolDto);

        Task<GetSchoolByIdResponseModel> GetSchoolById(int Id);

        Task<GetAllSchoolByFiltersResponseModel> GetSchoolByFilters(string SchoolName, string InstituteName,string City,string State, DateTime CreatedDate, bool IsActive);

        Task<GetAllSchoolByPaginationResponseModel> GetSchoolByPagination(int page, int size);

        Task<UpdateSchoolResponseModel> UpdateSchool(UpdateSchoolDto updateSchoolDto);
        #endregion

    }
}
