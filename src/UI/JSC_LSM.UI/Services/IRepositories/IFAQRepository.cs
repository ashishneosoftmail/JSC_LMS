using JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ;
using JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
  public  interface IFAQRepository
    {
        Task<AddFAQBaseResponseModel> AddFAQ(CreateFAQDto createFAQDto);
        Task<UpdateFAQResponseModel> EditFAQ(UpdateFAQDto updateFAQDto);
        Task<GetFAQByIdResponseModel> GetFAQById(int id);

        Task<GetAllFAQPaginationResponseModel> GetAllFAQByPagination(int page, int size);
        Task<GetAllFAQFiltersResponseModel> GetAllFAQByFilters(string faqtitle, bool isactive, string category);
        Task<GetAllFAQListResponseModel> GetAllFAQList();
        Task DeleteFAQ(int id);
    }
}
