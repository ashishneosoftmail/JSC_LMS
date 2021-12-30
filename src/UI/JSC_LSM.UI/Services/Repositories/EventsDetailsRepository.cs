using JSC_LMS.Application.Features.EventsFeature.Commands.CreateEvents;
using JSC_LMS.Application.Features.EventsFeature.Commands.UpdateEvents;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.ResponseModels.EventsResponseModel;
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
    public class EventsDetailsRepository : IEventsDetailsRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;

        public EventsDetailsRepository()
        {

        }

        public async Task<AddEventsResponseModel> AddEventsData(string baseurl, CreateEventsDto createEventsDto)
        {
            AddEventsResponseModel addEventsResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createEventsDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddEventsData, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                addEventsResponseModel = JsonConvert.DeserializeObject<AddEventsResponseModel>(_oApiResponse.data);
                if (addEventsResponseModel.Succeeded)
                {
                    addEventsResponseModel.Succeeded = true;
                }
                else
                {
                    addEventsResponseModel.Succeeded = false;
                }
            }

            return addEventsResponseModel;
        }

        public async Task<GetEventsListResponseModel> GetEventsList(string baseurl)
        {
            GetEventsListResponseModel getAllEventsListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllEventsList, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllEventsListResponseModel = JsonConvert.DeserializeObject<GetEventsListResponseModel>(_oApiResponse.data);
                getAllEventsListResponseModel.Succeeded = true;
            }

            return getAllEventsListResponseModel;
        }

        public async Task<GetEventsByIdResponseModel> GetEventsById(string baseurl, int Id)
        {
            GetEventsByIdResponseModel getEventsByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetEventsById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getEventsByIdResponseModel = JsonConvert.DeserializeObject<GetEventsByIdResponseModel>(_oApiResponse.data);
                getEventsByIdResponseModel.Succeeded = true;
            }

            return getEventsByIdResponseModel;
        }

        public async Task<UpdateEventsResponseModel> UpdateEventsDetails(string baseurl, UpdateEventsDto updateEventsDto)
        {
            UpdateEventsResponseModel updateEventsResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateEventsDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.EditEventsDetails, HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateEventsResponseModel = JsonConvert.DeserializeObject<UpdateEventsResponseModel>(_oApiResponse.data);
                if (updateEventsResponseModel.Succeeded)
                {
                    updateEventsResponseModel.Succeeded = true;
                }
                else
                {
                    updateEventsResponseModel.Succeeded = false;
                }
            }

            return updateEventsResponseModel;
        }

        public async Task<GetAllEventsListBySchoolResponseModel> GetAllEventsBySchoolList(string baseurl, int schoolid)
        {
            GetAllEventsListBySchoolResponseModel getAllEventsListBySchoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllEventsBySchool + $"?schoolid={schoolid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllEventsListBySchoolResponseModel = JsonConvert.DeserializeObject<GetAllEventsListBySchoolResponseModel>(_oApiResponse.data);
                getAllEventsListBySchoolResponseModel.Succeeded = true;
            }

            return getAllEventsListBySchoolResponseModel;
        }

    }
}
