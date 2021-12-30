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
        Task<AddFAQBaseResponseModel> AddFAQ(string baseurl, CreateFAQDto createFAQDto);
        Task<UpdateFAQResponseModel> EditFAQ(string baseurl, UpdateFAQDto updateFAQDto);
        Task<GetFAQByIdResponseModel> GetFAQById(string baseurl, int id);

        Task<GetAllFAQPaginationResponseModel> GetAllFAQByPagination(string baseurl, int page, int size);
        Task<GetAllFAQFiltersResponseModel> GetAllFAQByFilters(string baseurl, string faqtitle, bool isactive, string category);
        Task<GetAllFAQListResponseModel> GetAllFAQList(string baseurl);
        Task DeleteFAQ(string baseurl, int id);
    }
}
