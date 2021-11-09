using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IInstituteRepository
    {
        Task<InstituteResponseModel> CreateInstitute(CreateInstituteDto createInstituteDto);
        Task<GetAllInstituteListResponseModel> GetAllInstituteDetails();
        Task<GetInstituteByIdResponseModel> GetInstituteById(int Id);

        Task<GetAllInstituteByFiltersResponseModel> GetInstituteByFilters(string InstituteName, string City, string State, DateTime LicenseExpiry, bool IsActive);

        Task<GetAllInstituteByPaginationResponseModel> GetInstituteByPagination(int page, int size);

        Task<UpdateInstituteResponseModel> UpdateInstitute(UpdateInstituteDto updateInstituteDto);
    }
}
