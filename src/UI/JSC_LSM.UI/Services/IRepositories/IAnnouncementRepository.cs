using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IAnnouncementRepository
    {
        Task<AddAnnouncementResponseModel> AddAnnouncement(CreateAnnouncementDto createAnnouncementDto);
        Task<GetAnnouncementListByPaginationResponseModel> GetAnnouncementListByPagination(int page, int size);
        Task<GetAnnouncementListResponseModel> GetAnnouncementList();
    }
}
