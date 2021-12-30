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
        Task<AddCircularResponseModel> AddCircular(string baseurl, CreateCircularDto createCircularDto);
        Task<GetAllCircularListByPaginationResponseModel> GetAllCircularListByPagination(string baseurl, int page, int size);
        Task<GetAllCircularListResponseModel> GetAllCircularList(string baseurl);
        Task<GetCircularByIdResponseModel> GetCircularById(string baseurl, int id);
        Task DeleteCircular(string baseurl, int id);
        Task<GetAllCircularByFilterInstituteAdminResponseModel> GetAllCircularByFilterInstituteAdmin(string baseurl, string circularTitle, string description, bool status);
        Task<UpdateCircularResponseModel> EditCircular(string baseurl, UpdateCircularDto updateCircular);
        Task<GetAllCircularListBySchoolPaginationResponseModel> GetCircularListBySchoolPagination(string baseurl, int page, int size, int schoolid);
        Task<GetAllCircularListBySchoolResponseModel> GetAllCircularBySchoolList(string baseurl, int schoolid);
        Task<GetAllCircularListByFilterAndSchoolResponseModel> GetAllCircularListByFilterAndSchool(string baseurl, string circularTitle, string description, bool status, int schoolid);
        Task<GetCircularByFilterSchoolAndCreatedDateResponseModel> GetAllCircularListByFilterSchoolAndCreatedDate(string baseurl, string circularTitle, string description, DateTime createdDate, int schoolid);
    }
}
