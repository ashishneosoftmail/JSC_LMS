using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
using JSC_LMS.Application.Features.Circulars.Commands.UpdateCircular;
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
    public class CircularRepository : ICircularRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public CircularRepository()
        {

        }

        public async Task<AddCircularResponseModel> AddCircular(string baseurl, CreateCircularDto createCircularDto)
        {
            AddCircularResponseModel addCircularResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createCircularDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddCircular, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                addCircularResponseModel = JsonConvert.DeserializeObject<AddCircularResponseModel>(_oApiResponse.data);
                if (addCircularResponseModel.Succeeded)
                {
                    addCircularResponseModel.Succeeded = true;
                }
                else
                {
                    addCircularResponseModel.Succeeded = false;
                }
            }

            return addCircularResponseModel;
        }

        public async Task<GetAllCircularListByPaginationResponseModel> GetAllCircularListByPagination(string baseurl, int page, int size)
        {
            GetAllCircularListByPaginationResponseModel getAllCircularListByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllCircularByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularListByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllCircularListByPaginationResponseModel>(_oApiResponse.data);
                getAllCircularListByPaginationResponseModel.Succeeded = true;
            }

            return getAllCircularListByPaginationResponseModel;
        }

        public async Task<GetAllCircularListResponseModel> GetAllCircularList(string baseurl)
        {
            GetAllCircularListResponseModel getAllCircularListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllCircularList, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularListResponseModel = JsonConvert.DeserializeObject<GetAllCircularListResponseModel>(_oApiResponse.data);
                getAllCircularListResponseModel.Succeeded = true;
            }

            return getAllCircularListResponseModel;
        }

        public async Task<GetCircularByIdResponseModel> GetCircularById(string baseurl, int Id)
        {
            GetCircularByIdResponseModel getCircularByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetCircularById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getCircularByIdResponseModel = JsonConvert.DeserializeObject<GetCircularByIdResponseModel>(_oApiResponse.data);
                getCircularByIdResponseModel.Succeeded = true;
            }

            return getCircularByIdResponseModel;
        }

        public async Task DeleteCircular(string baseurl, int id)
        {

            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.DeleteCircularById + id, HttpMethod.Delete, bytes, _sToken);
        }

        public async Task<GetAllCircularByFilterInstituteAdminResponseModel> GetAllCircularByFilterInstituteAdmin(string baseurl, string circularTitle, string description, bool status)
        {
            GetAllCircularByFilterInstituteAdminResponseModel getAllCircularByFilterInstituteAdminResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetCircularByFilerInstituteAdmin + $"?_CircularTitle={circularTitle}&_Description={description}&_Status={status}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularByFilterInstituteAdminResponseModel = JsonConvert.DeserializeObject<GetAllCircularByFilterInstituteAdminResponseModel>(_oApiResponse.data);
                getAllCircularByFilterInstituteAdminResponseModel.Succeeded = true;
            }

            return getAllCircularByFilterInstituteAdminResponseModel;
        }

        public async Task<UpdateCircularResponseModel> EditCircular(string baseurl, UpdateCircularDto updateCircular)
        {
            UpdateCircularResponseModel updateCircularResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateCircular, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.EditCircular, HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateCircularResponseModel = JsonConvert.DeserializeObject<UpdateCircularResponseModel>(_oApiResponse.data);
                if (updateCircularResponseModel.Succeeded)
                {
                    updateCircularResponseModel.Succeeded = true;
                }
                else
                {
                    updateCircularResponseModel.Succeeded = false;
                }
            }

            return updateCircularResponseModel;
        }

        public async Task<GetAllCircularListBySchoolPaginationResponseModel> GetCircularListBySchoolPagination(string baseurl, int page, int size, int schoolid)
        {
            GetAllCircularListBySchoolPaginationResponseModel getAllCircularListBySchoolPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetCircularListBySchoolPagination + $"?_page={page}&_size={size}&_schoolId={schoolid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularListBySchoolPaginationResponseModel = JsonConvert.DeserializeObject<GetAllCircularListBySchoolPaginationResponseModel>(_oApiResponse.data);
                getAllCircularListBySchoolPaginationResponseModel.Succeeded = true;
            }

            return getAllCircularListBySchoolPaginationResponseModel;
        }

        public async Task<GetAllCircularListBySchoolResponseModel> GetAllCircularBySchoolList(string baseurl, int schoolid)
        {
            GetAllCircularListBySchoolResponseModel getAllCircularListBySchoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllCircularBySchool + $"?schoolid={schoolid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularListBySchoolResponseModel = JsonConvert.DeserializeObject<GetAllCircularListBySchoolResponseModel>(_oApiResponse.data);
                getAllCircularListBySchoolResponseModel.Succeeded = true;
            }

            return getAllCircularListBySchoolResponseModel;
        }

        public async Task<GetAllCircularListByFilterAndSchoolResponseModel> GetAllCircularListByFilterAndSchool(string baseurl, string circularTitle, string description, bool status, int schoolid)
        {
            GetAllCircularListByFilterAndSchoolResponseModel getAllCircularListByFilterAndSchoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetCircularByFilterAndSchool + $"?_CircularTitle={circularTitle}&_Description={description}&_Status={status}&schoolid={schoolid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularListByFilterAndSchoolResponseModel = JsonConvert.DeserializeObject<GetAllCircularListByFilterAndSchoolResponseModel>(_oApiResponse.data);
                getAllCircularListByFilterAndSchoolResponseModel.Succeeded = true;
            }

            return getAllCircularListByFilterAndSchoolResponseModel;
        }

        public async Task<GetCircularByFilterSchoolAndCreatedDateResponseModel> GetAllCircularListByFilterSchoolAndCreatedDate(string baseurl, string circularTitle, string description, DateTime createdDate, int schoolid)
        {
            GetCircularByFilterSchoolAndCreatedDateResponseModel getCircularByFilterSchoolAndCreatedDateResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetCircularByFilterSchoolAndCreatedDate + $"?_CircularTitle={circularTitle}&_Description={description}&_CreatedDate={createdDate:yyyy/MM/dd}&schoolid={schoolid}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getCircularByFilterSchoolAndCreatedDateResponseModel = JsonConvert.DeserializeObject<GetCircularByFilterSchoolAndCreatedDateResponseModel>(_oApiResponse.data);
                getCircularByFilterSchoolAndCreatedDateResponseModel.Succeeded = true;
            }

            return getCircularByFilterSchoolAndCreatedDateResponseModel;
        }
    }
}
