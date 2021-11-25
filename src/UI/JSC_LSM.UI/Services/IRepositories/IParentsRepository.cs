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
        Task<ParentsResponseModel> AddNewParents(CreateParentsDto createParentsDto);
        Task<UpdateParentsResponseModel> UpdateParents(UpdateParentsDto updateParentsDto);
        Task<GetParentsByIdResponseModel> GetParentsById(int Id);

        Task<GetAllParentsListResponseModel> GetAllParentsDetails();

        Task<GetAllParentsByFiltersResponseModel> GetParentsByFilters(string ClassName, string SectionName, string StudentName, string ParentName,bool IsActive, DateTime CreatedDate);

        Task<GetAllParentsByPaginationResponseModel> GetParentsByPagination(int page, int size);

        Task<GetParentByUserIdResponseModel> GetParentByUserId(string UserId);

    }
}
