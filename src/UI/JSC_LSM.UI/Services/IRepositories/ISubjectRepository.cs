using JSC_LMS.Application.Features.Subject.Commands.CreateSubject;
using JSC_LMS.Application.Features.Subject.Commands.UpdateSubject;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
  public  interface ISubjectRepository
    {
        Task<SubjectResponseModel> AddNewSubject(string baseurl, CreateSubjectDto createSubjectDto);

        Task<GetSubjectByIdResponseModel> GetSubjectById(string baseurl, int Id);

        Task<GetAllSubjectListResponseModel> GetAllSubjectDetails(string baseurl);
        Task<GetAllSubjectByFiltersResponseModel> GetSubjectByFilters(string baseurl, string SchoolName, string ClassName, string SectionName, string SubjectName, DateTime CreatedDate, bool IsActive);

        Task<GetAllSubjectByPaginationResponseModel> GetSubjectByPagination(string baseurl, int page, int size);

        Task<UpdateSubjectResponseModel> UpdateSubject(string baseurl, UpdateSubjectDto updateSubjectDto);
    }
}
