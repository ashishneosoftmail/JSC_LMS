using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using JSC_LMS.Application.Features.Students.Commands.UpdateStudent;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IStudentRepository
    {
        Task<GetAllStudentListResponseModel> GetAllStudentDetails(string baseurl);

        Task<GetStudentByIdResponseModel> GetStudentById(string baseurl, int Id);

        Task<GetAllStudentByFiltersResponseModel> GetStudentByFilters(string baseurl, int SchoolId , string ClassName, string SectionName,string StudentName, bool IsActive, DateTime CreatedDate);

        Task<GetAllStudentByPaginationResponseModel> GetStudentByPagination(string baseurl, int page, int size);

        Task<UpdateStudentResponseModel> UpdateStudent(string baseurl, UpdateStudentDto updateStudentDto);

        Task<StudentResponseModel> AddNewStudent(string baseurl, CreateStudentDto createStudentDto);

        Task<GetStudentByUserIdResponseModel> GetStudentByUserId(string baseurl, string UserId);
        Task<GetAllStudentListBySchoolPaginationResponseModel> GetStudentListBySchoolPagination(string baseurl, int page, int size, int schoolid);
        Task<GetAllStudentListBySchoolResponseModel> GetAllStudentBySchoolList(string baseurl, int schoolid);

    }
}
