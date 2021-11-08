using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
  public  interface IClassRepository
    {
        Task<ClassResponseModel> AddNewClass(CreateClassDto createClassDto);
       
        Task<GetClassByIdResponseModel> GetClassById(int Id);

        Task<GetAllClassListResponseModel> GetAllClassDetails();
        Task<GetAllClassByFiltersResponseModel> GetClassByFilters(string SchoolName, string ClassName, DateTime CreatedDate, bool IsActive);

        Task<GetAllClassByPaginationResponseModel> GetClassByPagination(int page, int size);
    }
}
