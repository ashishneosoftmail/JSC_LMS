using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Features.Announcement.Commands.CreateAnnouncement;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
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
            GetTeacherByIdResponseModel getTeacherByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetTeacherById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getTeacherByIdResponseModel = JsonConvert.DeserializeObject<GetTeacherByIdResponseModel>(_oApiResponse.data);
                getTeacherByIdResponseModel.Succeeded = true;
            }

            return getTeacherByIdResponseModel;

        }

    }
}
