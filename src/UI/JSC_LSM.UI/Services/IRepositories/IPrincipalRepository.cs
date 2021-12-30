using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    #region -Developed By Harsh Chheda
    public interface IPrincipalRepository
    {
        /// <summary>
        /// Returns all the principal data - Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        Task<GetAllPrincipalListResponseModel> GetAllPrincipalDetails(string baseurl);
        /// <summary>
        /// Returns the principal data based on id - Developed By Harsh Chheda
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<GetPrincipalByIdResponseModel> GetPrincipalById(string baseurl, int Id);
        /// <summary>
        /// Returns the principal data based on the search parameters - Developed By Harsh Chheda
        /// </summary>
        /// <param name="SchoolName"></param>
        /// <param name="PrincipalName"></param>
        /// <param name="CreatedDate"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        Task<GetAllPrincipalByFiltersResponseModel> GetPrincipalByFilters(string baseurl, string SchoolName, string PrincipalName, DateTime CreatedDate, bool IsActive);
        /// <summary>
        /// Returns the principal data based on the page and no size of rows - Developed By Harsh Chheda
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<GetAllPrincipalByPaginationResponseModel> GetPrincipalByPagination(string baseurl, int page, int size);
        /// <summary>
        /// Upadate the Principal data - Developed By Harsh Chheda
        /// </summary>
        /// <param name="updatePrincipalDto"></param>
        /// <returns></returns>
        Task<UpdatePrincipalResponseModel> UpdatePrincipal(string baseurl, UpdatePrincipalDto updatePrincipalDto);
        /// <summary>
        /// Adds a new principal - Developed By Harsh Chheda
        /// </summary>
        /// <param name="createPrincipalDto"></param>
        /// <returns></returns>
        Task<PrincipalResponseModel> AddNewPrinicipal(string baseurl, CreatePrincipalDto createPrincipalDto);

        Task<GetPrincipalByUserIdResponseModel> GetPrincipalByUserId(string baseurl, string UserId);

    }
    #endregion
}
