using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
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
    }
}
