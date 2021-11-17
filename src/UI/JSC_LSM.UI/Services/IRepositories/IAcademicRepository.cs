using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
  public  interface IAcademicRepository
    {
        Task<AcademicResponseModel> AddNewAcademic(CreateAcademicDto createAcademicDto);
        Task<GetAcademicByIdResponseModel> GetAcademicById(int Id);

        Task<GetAllAcademicListResponseModel> GetAllAcademicDetails();
        Task<GetAllAcademicByFiltersResponseModel> GetAcademicByFilters(string SchoolName, string ClassName, string SectionName, string SubjectName,string TeacherName,string Type, DateTime CreatedDate, bool IsActive);

        Task<GetAllAcademicByPaginationResponseModel> GetAcademicByPagination(int page, int size);

        Task<UpdateAcademicResponseModel> UpdateAcademic(UpdateAcademicDto updateAcademicDto);
    }
}
