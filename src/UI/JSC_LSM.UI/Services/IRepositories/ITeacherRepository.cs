using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
   public  interface ITeacherRepository
    {
        Task<TeacherResponseModel> CreateTeacher(string baseurl, CreateTeacherDto createTeacherDto);
        //Task<GetAllInstituteListResponseModel> GetAllInstituteDetails();

        /// <summary>
        /// Upadate the teacher data:by Shivani Goswami
        /// </summary>
        /// <param name="updateTeacherDto"></param>
        /// <returns></returns>
        Task<UpdateTeacherResponseModel> UpdateTeacher(string baseurl, UpdateTeacherDto updateTeacherDto);

        Task<GetTeacherByIdResponseModel> GetTeacherById(string baseurl, int Id);

        Task<GetAllTeacherByPaginationResponseModel> GetTeacherByPagination(string baseurl, int page, int size);

        Task<GetAllTeacherListResponseModel> GetAllTeacherDetails(string baseurl);
        Task<GetAllTeacherByFiltersResponseModel> GetTeacherByFilters(string baseurl, string SchoolName, string ClassName, string SectionName, string SubjectName, string TeacherName, DateTime CreatedDate, bool IsActive);

        Task<GetTeacherByUserIdResponseModel> GetTeacherByUserId(string baseurl, string UserId);
    }
}
