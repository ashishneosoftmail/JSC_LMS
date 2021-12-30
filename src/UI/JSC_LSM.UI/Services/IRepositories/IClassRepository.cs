using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
  public  interface IClassRepository
    {
        Task<ClassResponseModel> AddNewClass(string baseurl, CreateClassDto createClassDto);
       
        Task<GetClassByIdResponseModel> GetClassById(string baseurl, int Id);

        Task<GetAllClassListResponseModel> GetAllClassDetails(string baseurl);
        Task<GetAllClassByFiltersResponseModel> GetClassByFilters(string baseurl, string SchoolName, string ClassName, DateTime CreatedDate, bool IsActive);

        Task<GetAllClassByPaginationResponseModel> GetClassByPagination(string baseurl, int page, int size);

        Task<UpdateClassResponseModel> UpdateClass(string baseurl, UpdateClassDto updateClassDto);

        Task<GetAllClassResponseModel> GetAllClass(string baseurl);
    }
}
