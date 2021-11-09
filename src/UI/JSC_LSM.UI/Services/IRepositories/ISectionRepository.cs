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
    
        Task<SectionResponseModel> AddNewSection(CreateSectionDto createSectionDto);

        Task<GetSectionByIdResponseModel> GetSectionById(int Id);

        Task<GetAllSectionListResponseModel> GetAllSectionDetails();
        Task<GetAllSectionByFiltersResponseModel> GetSectionByFilters(string SchoolName, string ClassName, string SectionName, DateTime CreatedDate, bool IsActive);

        Task<GetAllSectionByPaginationResponseModel> GetSectionByPagination(int page, int size);

        Task<UpdateSectionResponseModel> UpdateSection(UpdateSectionDto updateSectionDto);

    }
}
