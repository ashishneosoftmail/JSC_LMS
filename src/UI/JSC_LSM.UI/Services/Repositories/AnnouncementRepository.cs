
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

        public async Task<GetAnnouncementByFiltersResponseModel> GetAnnouncementByFilters(string SchoolName, string ClassName, string SectionName, string SubjectName, string TeacherName, string AnnouncementMadeBy, string AnnouncementTitle, string AnnouncementContent, DateTime CreatedDate)
        {
            GetAnnouncementByFiltersResponseModel getAnnouncementByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);          
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Announcement/Filter?SchoolName ={SchoolName}&ClassName={ClassName}&SectionName={SectionName}&SubjectName={SubjectName}&TeacherName={TeacherName}&AnnouncementMadeBy={AnnouncementMadeBy}&AnnouncementTitle={AnnouncementTitle}&AnnouncementContent={AnnouncementContent}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAnnouncementByFiltersResponseModel = JsonConvert.DeserializeObject<GetAnnouncementByFiltersResponseModel>(_oApiResponse.data);
                getAnnouncementByFiltersResponseModel.Succeeded = true;
            }

            return getAnnouncementByFiltersResponseModel;

        }

    }
}
