using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
using JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface ICircularRepository
    {
        Task<AddCircularResponseModel> AddCircular(CreateCircularDto createCircularDto);
        Task<GetAllCircularListByPaginationResponseModel> GetAllCircularListByPagination(int page, int size);
        Task<GetAllCircularListResponseModel> GetAllCircularList();
        Task<GetCircularByIdResponseModel> GetCircularById(int id);
        Task DeleteCircular(int id);
        Task<GetAllCircularByFilterInstituteAdminResponseModel> GetAllCircularByFilterInstituteAdmin(string circularTitle, string description, bool status);
        Task<UpdateCircularResponseModel> EditCircular(UpdateCircularDto updateCircular);
        Task<GetAllCircularListBySchoolPaginationResponseModel> GetCircularListBySchoolPagination(int page, int size, int schoolid);
        Task<GetAllCircularListBySchoolResponseModel> GetAllCircularBySchoolList(int schoolid);
        Task<GetAllCircularListByFilterAndSchoolResponseModel> GetAllCircularListByFilterAndSchool(string circularTitle, string description, bool status, int schoolid);
        Task<GetCircularByFilterSchoolAndCreatedDateResponseModel> GetAllCircularListByFilterSchoolAndCreatedDate(string circularTitle, string description, DateTime createdDate, int schoolid);
    }
}
