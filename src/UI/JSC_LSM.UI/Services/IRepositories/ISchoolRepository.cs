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
        Task<GetAllSchoolResponseModel> GetAllSchool(string baseurl);

        Task<SchoolResponseModel> AddNewSchool(string baseurl, CreateSchoolDto createSchoolDto);

        Task<GetSchoolByIdResponseModel> GetSchoolById(string baseurl, int Id);

        Task<GetAllSchoolByFiltersResponseModel> GetSchoolByFilter(string baseurl, string SchoolName, string InstituteName,string City,string State, bool IsActive,DateTime CreatedDate);

        Task<GetAllSchoolByPaginationResponseModel> GetSchoolByPagination(string baseurl, int page, int size);

        Task<UpdateSchoolResponseModel> UpdateSchool(string baseurl, UpdateSchoolDto updateSchoolDto);
        #endregion

    }
}
