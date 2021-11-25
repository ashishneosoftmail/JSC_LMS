using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
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
    }
}
