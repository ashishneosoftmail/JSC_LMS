using JSC_LMS.Application.Features.Common.Categories.Commands;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface ICategoryRepository
    {
        Task<CreateCategoryResponseModel> AddCategory(string baseurl, CreateCategoryDto createCategoryDto);
        Task<GetAllCategoryResponseModel> GetAllCategory(string baseurl);
    }
}
