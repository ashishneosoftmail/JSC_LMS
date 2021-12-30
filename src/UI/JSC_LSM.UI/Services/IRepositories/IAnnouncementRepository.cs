using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IAnnouncementRepository
    {
        Task<AddAnnouncementResponseModel> AddAnnouncement(string baseurl, CreateAnnouncementDto createAnnouncementDto);
        Task<GetAnnouncementListByPaginationResponseModel> GetAnnouncementListByPagination(string baseurl, int page, int size);
        Task<GetAnnouncementListResponseModel> GetAnnouncementList(string baseurl);
        Task<GetAnnouncementByIdResponseModel> GetAnnouncementById(string baseurl, int Id);
        Task<UpdateAnnouncementResponseModel> UpdateAnnouncement(string baseurl, UpdateAnnouncementDto updateAnnouncementDto);
        Task<GetAnnouncementByFiltersResponseModel> GetAnnouncementByFilters(string baseurl, int SchoolId, int ClassId, int SectionId, int SubjectId, string TeacherName, string AnnouncementMadeBy, string AnnouncementTitle, string AnnouncementContent, DateTime CreatedDate);

        Task<GetAllAnnouncementListBySchoolPaginationResponseModel> GetAnnouncementListBySchoolPagination(string baseurl, int page, int size, int schoolid);
        Task<GetAllAnnouncementListBySchoolResponseModel> GetAllAnnouncementBySchoolList(string baseurl, int schoolid);
        Task<GetAllAnnouncementListBySchoolClassSectionResponseModel> GetAllAnnouncementBySchoolClassSectionList(string baseurl, int schoolid , int classid,int sectionid);
        Task<GetAllAnnouncementListBySchoolClassSectionPaginationResponseModel> GetAnnouncementListBySchoolClassSectionPagination(string baseurl, int page, int size, int schoolid , int classid, int sectionid);
    }
}
