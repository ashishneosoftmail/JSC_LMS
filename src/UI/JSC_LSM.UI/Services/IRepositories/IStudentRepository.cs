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
        Task<GetAllStudentListResponseModel> GetAllStudentDetails();

        Task<GetStudentByIdResponseModel> GetStudentById(int Id);

        Task<GetAllStudentByFiltersResponseModel> GetStudentByFilters(int SchoolId , string ClassName, string SectionName,string StudentName, bool IsActive, DateTime CreatedDate);

        Task<GetAllStudentByPaginationResponseModel> GetStudentByPagination(int page, int size);

        Task<UpdateStudentResponseModel> UpdateStudent(UpdateStudentDto updateStudentDto);

        Task<StudentResponseModel> AddNewStudent(CreateStudentDto createStudentDto);

        Task<GetStudentByUserIdResponseModel> GetStudentByUserId(string UserId);
        Task<GetAllStudentListBySchoolPaginationResponseModel> GetStudentListBySchoolPagination(int page, int size, int schoolid);
        Task<GetAllStudentListBySchoolResponseModel> GetAllStudentBySchoolList(int schoolid);

    }
}
