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
        Task<TeacherResponseModel> CreateTeacher(CreateTeacherDto createTeacherDto);
        //Task<GetAllInstituteListResponseModel> GetAllInstituteDetails();

        /// <summary>
        /// Upadate the teacher data:by Shivani Goswami
        /// </summary>
        /// <param name="updateTeacherDto"></param>
        /// <returns></returns>
        Task<UpdateTeacherResponseModel> UpdateTeacher(UpdateTeacherDto updateTeacherDto);

        Task<GetTeacherByIdResponseModel> GetTeacherById(int Id);

        Task<GetAllTeacherByPaginationResponseModel> GetTeacherByPagination(int page, int size);

        Task<GetAllTeacherListResponseModel> GetAllTeacherDetails();

        //Task<GetAllTeacherByFiltersResponseModel> GetTeacherByFilters(string SchoolName, string PrincipalName, DateTime CreatedDate, bool IsActive);
    }
}
