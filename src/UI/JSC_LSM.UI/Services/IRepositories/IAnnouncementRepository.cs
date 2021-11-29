﻿using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
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
        Task<AddAnnouncementResponseModel> AddAnnouncement(CreateAnnouncementDto createAnnouncementDto);
        Task<GetAnnouncementListByPaginationResponseModel> GetAnnouncementListByPagination(int page, int size);
        Task<GetAnnouncementListResponseModel> GetAnnouncementList();
        Task<GetAnnouncementByIdResponseModel> GetAnnouncementById(int Id);
        Task<UpdateAnnouncementResponseModel> UpdateAnnouncement(UpdateAnnouncementDto updateAnnouncementDto);
        Task<GetAnnouncementByFiltersResponseModel> GetAnnouncementByFilters(int SchoolId, int ClassId, int SectionId, int SubjectId, string TeacherName, string AnnouncementMadeBy, string AnnouncementTitle, string AnnouncementContent, DateTime CreatedDate);

        Task<GetAllAnnouncementListBySchoolPaginationResponseModel> GetAnnouncementListBySchoolPagination(int page, int size, int schoolid);
        Task<GetAllAnnouncementListBySchoolResponseModel> GetAllAnnouncementBySchoolList(int schoolid);
        Task<GetAllAnnouncementListBySchoolClassSectionResponseModel> GetAllAnnouncementBySchoolClassSectionList(int schoolid , int classid,int sectionid);
        Task<GetAllAnnouncementListBySchoolClassSectionPaginationResponseModel> GetAnnouncementListBySchoolClassSectionPagination(int page, int size, int schoolid , int classid, int sectionid);
    }
}
