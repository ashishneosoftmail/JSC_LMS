using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IPrincipalRepository
    {
        Task<GetAllPrincipalListResponseModel> GetAllPrincipalDetails();
        Task<GetPrincipalByIdResponseModel> GetPrincipalById(int Id);
        Task<GetAllPrincipalByFiltersResponseModel> GetPrincipalByFilters(string SchoolName, string PrincipalName, DateTime CreatedDate, bool IsActive);
        Task<GetAllPrincipalByPaginationResponseModel> GetPrincipalByPagination(int page, int size);
        Task<UpdatePrincipalResponseModel> UpdatePrincipal(UpdatePrincipalViewModel updatePrincipalViewModel);
    }
}
