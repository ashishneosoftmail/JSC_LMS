using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LMS.Application.Features.Section.Commands.CreateUpdate;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
   public interface ISectionRepository
    {
    
        Task<SectionResponseModel> AddNewSection(string baseurl, CreateSectionDto createSectionDto);

        Task<GetSectionByIdResponseModel> GetSectionById(string baseurl, int Id);

        Task<GetAllSectionListResponseModel> GetAllSectionDetails(string baseurl);
        Task<GetAllSectionByFiltersResponseModel> GetSectionByFilters(string baseurl, string SchoolName, string ClassName, string SectionName, DateTime CreatedDate, bool IsActive);

        Task<GetAllSectionByPaginationResponseModel> GetSectionByPagination(string baseurl, int page, int size);

        Task<UpdateSectionResponseModel> UpdateSection(string baseurl, UpdateSectionDto updateSectionDto);

        Task<GetAllSectionResponseModel> GetAllSection(string baseurl);

    }
}
