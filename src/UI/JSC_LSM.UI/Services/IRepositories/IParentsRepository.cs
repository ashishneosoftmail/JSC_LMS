using JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents;
using JSC_LMS.Application.Features.ParentsFeature.Commands.UpdateParents;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IParentsRepository
    {
        Task<ParentsResponseModel> AddNewParents(string baseurl, CreateParentsDto createParentsDto);
        Task<UpdateParentsResponseModel> UpdateParents(string baseurl, UpdateParentsDto updateParentsDto);
        Task<GetParentsByIdResponseModel> GetParentsById(string baseurl, int Id);

        Task<GetAllParentsListResponseModel> GetAllParentsDetails(string baseurl);

        Task<GetAllParentsByFiltersResponseModel> GetParentsByFilters(string baseurl, int SchoolId , string ClassName, string SectionName, string StudentName, string ParentName,bool IsActive, DateTime CreatedDate);

        Task<GetAllParentsByPaginationResponseModel> GetParentsByPagination(string baseurl, int page, int size);

        Task<GetParentByUserIdResponseModel> GetParentByUserId(string baseurl, string UserId);
        Task<GetAllParentsListBySchoolPaginationResponseModel> GetParentsListBySchoolPagination(string baseurl, int page, int size, int schoolid);
        Task<GetAllParentsListBySchoolResponseModel> GetAllParentsBySchoolList(string baseurl, int schoolid);

    }
}
