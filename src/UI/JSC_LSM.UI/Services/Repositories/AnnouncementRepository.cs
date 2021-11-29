
using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LMS.Application.Features.Announcement.Commands.UpdateAnnouncement;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public AnnouncementRepository()
        {

        }

        public async Task<AddAnnouncementResponseModel> AddAnnouncement(CreateAnnouncementDto createAnnouncementDto)
        {
            AddAnnouncementResponseModel addAnnouncementResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createAnnouncementDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddAnnouncement, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                addAnnouncementResponseModel = JsonConvert.DeserializeObject<AddAnnouncementResponseModel>(_oApiResponse.data);
                if (addAnnouncementResponseModel.Succeeded)
                {
                    addAnnouncementResponseModel.Succeeded = true;
                }
                else
                {
                    addAnnouncementResponseModel.Succeeded = false;
                }
            }

            return addAnnouncementResponseModel;
        }

        public async Task<GetAnnouncementListByPaginationResponseModel> GetAnnouncementListByPagination(int page, int size)
        {
            GetAnnouncementListByPaginationResponseModel getAnnouncementListByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAnnouncementByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAnnouncementListByPaginationResponseModel = JsonConvert.DeserializeObject<GetAnnouncementListByPaginationResponseModel>(_oApiResponse.data);
                getAnnouncementListByPaginationResponseModel.Succeeded = true;
            }

            return getAnnouncementListByPaginationResponseModel;
        }

        public async Task<GetAnnouncementListResponseModel> GetAnnouncementList()
        {
            GetAnnouncementListResponseModel getAnnouncementListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAnnouncementList, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAnnouncementListResponseModel = JsonConvert.DeserializeObject<GetAnnouncementListResponseModel>(_oApiResponse.data);
                getAnnouncementListResponseModel.Succeeded = true;
            }

            return getAnnouncementListResponseModel;
        }

        public async Task<GetAnnouncementByIdResponseModel> GetAnnouncementById(int Id)
        {
            GetAnnouncementByIdResponseModel getAnnouncementByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAnnouncementById + "?id=" + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAnnouncementByIdResponseModel = JsonConvert.DeserializeObject<GetAnnouncementByIdResponseModel>(_oApiResponse.data);
                getAnnouncementByIdResponseModel.Succeeded = true;
            }

            return getAnnouncementByIdResponseModel;

        }
        public async Task<UpdateAnnouncementResponseModel> UpdateAnnouncement(UpdateAnnouncementDto updateAnnouncementDto)
        {
            UpdateAnnouncementResponseModel updateAnnouncementResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateAnnouncementDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication("/api/v1/Announcement/Update", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateAnnouncementResponseModel = JsonConvert.DeserializeObject<UpdateAnnouncementResponseModel>(_oApiResponse.data);
                if (updateAnnouncementResponseModel.Succeeded)
                {
                    updateAnnouncementResponseModel.Succeeded = true;
                }
                else
                {
                    updateAnnouncementResponseModel.Succeeded = false;
                }
            }

            return updateAnnouncementResponseModel;
        }

        public async Task<GetAnnouncementByFiltersResponseModel> GetAnnouncementByFilters(int SchoolId, int ClassId, int SectionId, int SubjectId, string TeacherName, string AnnouncementMadeBy, string AnnouncementTitle, string AnnouncementContent, DateTime CreatedDate)
        {
            GetAnnouncementByFiltersResponseModel getAnnouncementByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);          
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Announcement/Filter?SchoolId={SchoolId}&ClassId={ClassId}&SectionId={SectionId}&SubjectId={SubjectId}&TeacherName={TeacherName}&AnnouncementMadeBy={AnnouncementMadeBy}&AnnouncementTitle={AnnouncementTitle}&AnnouncementContent={AnnouncementContent}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAnnouncementByFiltersResponseModel = JsonConvert.DeserializeObject<GetAnnouncementByFiltersResponseModel>(_oApiResponse.data);
                getAnnouncementByFiltersResponseModel.Succeeded = true;
            }

            return getAnnouncementByFiltersResponseModel;

        }
        public async Task<GetAllAnnouncementListBySchoolPaginationResponseModel> GetAnnouncementListBySchoolPagination(int page, int size, int schoolid)
        {
            GetAllAnnouncementListBySchoolPaginationResponseModel getAllAnnouncementListBySchoolPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAnnouncementListBySchoolPagination + $"?_page={page}&_size={size}&_schoolId={schoolid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAnnouncementListBySchoolPaginationResponseModel = JsonConvert.DeserializeObject<GetAllAnnouncementListBySchoolPaginationResponseModel>(_oApiResponse.data);
                getAllAnnouncementListBySchoolPaginationResponseModel.Succeeded = true;
            }

            return getAllAnnouncementListBySchoolPaginationResponseModel;
        }

        public async Task<GetAllAnnouncementListBySchoolResponseModel> GetAllAnnouncementBySchoolList(int schoolid)
        {
            GetAllAnnouncementListBySchoolResponseModel getAllAnnouncementListBySchoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllAnnouncementBySchool + $"?schoolid={schoolid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAnnouncementListBySchoolResponseModel = JsonConvert.DeserializeObject<GetAllAnnouncementListBySchoolResponseModel>(_oApiResponse.data);
                getAllAnnouncementListBySchoolResponseModel.Succeeded = true;
            }

            return getAllAnnouncementListBySchoolResponseModel;
        }

        public async Task<GetAllAnnouncementListBySchoolClassSectionResponseModel> GetAllAnnouncementBySchoolClassSectionList(int schoolid, int classid, int sectionid)
        {
            GetAllAnnouncementListBySchoolClassSectionResponseModel getAllAnnouncementListBySchoolClassSectionResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);
           
            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllAnnouncementBySchoolClassSection + $"?schoolid={schoolid}&classid={classid}&sectionid={sectionid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAnnouncementListBySchoolClassSectionResponseModel = JsonConvert.DeserializeObject<GetAllAnnouncementListBySchoolClassSectionResponseModel>(_oApiResponse.data);
                getAllAnnouncementListBySchoolClassSectionResponseModel.Succeeded = true;
            }

            return getAllAnnouncementListBySchoolClassSectionResponseModel;
        }

        public async Task<GetAllAnnouncementListBySchoolClassSectionPaginationResponseModel> GetAnnouncementListBySchoolClassSectionPagination(int page, int size, int schoolid, int classid, int sectionid)
        {
            GetAllAnnouncementListBySchoolClassSectionPaginationResponseModel getAllAnnouncementListBySchoolClassSectionPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAnnouncementListBySchoolClassSectionPagination + $"?_page={page}&_size={size}&_schoolId={schoolid}&_classid={classid}&_sectionid={sectionid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAnnouncementListBySchoolClassSectionPaginationResponseModel = JsonConvert.DeserializeObject<GetAllAnnouncementListBySchoolClassSectionPaginationResponseModel>(_oApiResponse.data);
                getAllAnnouncementListBySchoolClassSectionPaginationResponseModel.Succeeded = true;
            }

            return getAllAnnouncementListBySchoolClassSectionPaginationResponseModel;
        }
    }
}
