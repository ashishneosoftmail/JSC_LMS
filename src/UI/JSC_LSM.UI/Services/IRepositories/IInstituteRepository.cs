using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminChangePassword;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminProfileInformation;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region-Interface repository for institute on UI end : by Shivani Goswami
namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IInstituteRepository
    {
        /// <summary>
        /// Creates a institute details for new institute: by Shivani Goswami
        /// </summary>
        /// <param name="createInstituteDto"></param>
        /// <returns></returns>
        Task<InstituteResponseModel> CreateInstitute(string baseurl, CreateInstituteDto createInstituteDto);
        /// <summary>
        /// Returns all the Institute data : by Shivani Goswami
        /// </summary>
        /// <returns></returns>
        Task<GetAllInstituteListResponseModel> GetAllInstituteDetails(string baseurl);
        /// <summary>
        /// Returns the Institute data based on id  : by Shivani Goswami
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<GetInstituteByIdResponseModel> GetInstituteById(string baseurl, int Id);
        /// <summary>
        /// Returns the institute data based on the search parameters : by Shivani Goswami
        /// </summary>
        /// <param name="InstituteName"></param>
        /// <param name="City"></param>
        /// <param name="State"></param>
        /// <param name="LicenseExpiry"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>

        Task<GetAllInstituteByFiltersResponseModel> GetInstituteByFilters(string baseurl, string InstituteName, string City, string State, DateTime LicenseExpiry, bool IsActive);
        /// <summary>
        /// Returns the institute data based on the page and no size of rows : by Shivani Goswami
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<GetAllInstituteByPaginationResponseModel> GetInstituteByPagination(string baseurl, int page, int size);
        /// <summary>
        /// Upadate the institute data:by Shivani Goswami
        /// </summary>
        /// <param name="updateInstituteDto"></param>
        /// <returns></returns>
        Task<UpdateInstituteResponseModel> UpdateInstitute(string baseurl, UpdateInstituteDto updateInstituteDto);
        Task<GetInstituteAdminByUserIdResponseModel> GetInstituteAdminByUserId(string baseurl, string UserId);
        Task<UpdateInstituteAdminProfileInformationResponseModel> UpdateInstituteAdminPersonalInformation(string baseurl, UpdateInstituteAdminProfileInformationDto updateInstituteAdminProfileInformationDto);

        Task<UpdateInstituteAdminChangePasswordResponseModel> UpdateInstituteAdminChangePassword(string baseurl, UpdateInstituteAdminChangePasswordDto updateInstituteAdminChangePasswordDto);
    }
}
#endregion