using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.School.Commands.CreateSchool;
using JSC_LMS.Application.Features.School.Commands.UpdateSchool;
using JSC_LMS.Application.Models.Authentication;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.ResponseModels.SchoolResponseModels;
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
    public class SchoolRepository : ISchoolRepository
    {

        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public SchoolRepository()
        {

        }
        #region- Developed By Harsh Chheda
        /// <summary>
        /// Returns all the school list - Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllSchoolResponseModel> GetAllSchool(string baseurl)
        {
            GetAllSchoolResponseModel getAllSchoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllSchool, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllSchoolResponseModel = JsonConvert.DeserializeObject<GetAllSchoolResponseModel>(_oApiResponse.data);
                getAllSchoolResponseModel.isSuccess = true;
            }

            return getAllSchoolResponseModel;

        }

        public async Task<SchoolResponseModel> AddNewSchool(string baseurl, CreateSchoolDto createSchoolDto)
        {
            SchoolResponseModel schoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createSchoolDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddNewSchool, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                schoolResponseModel = JsonConvert.DeserializeObject<SchoolResponseModel>(_oApiResponse.data);
                if (schoolResponseModel.Succeeded)
                {
                    schoolResponseModel.Succeeded = true;
                }
                else
                {
                    schoolResponseModel.Succeeded = false;
                }
            }

            return schoolResponseModel;

        }

        public async Task<GetSchoolByIdResponseModel> GetSchoolById(string baseurl, int Id)
        {
            GetSchoolByIdResponseModel getSchoolByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetSchoolById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getSchoolByIdResponseModel = JsonConvert.DeserializeObject<GetSchoolByIdResponseModel>(_oApiResponse.data);
                getSchoolByIdResponseModel.Succeeded = true;
            }

            return getSchoolByIdResponseModel;

        }

        public async Task<GetAllSchoolByPaginationResponseModel> GetSchoolByPagination(string baseurl, int page, int size)
        {
            GetAllSchoolByPaginationResponseModel getAllSchoolByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetSchoolByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllSchoolByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllSchoolByPaginationResponseModel>(_oApiResponse.data);
                getAllSchoolByPaginationResponseModel.Succeeded = true;
            }

            return getAllSchoolByPaginationResponseModel;

        }

        public async Task<UpdateSchoolResponseModel> UpdateSchool(string baseurl, UpdateSchoolDto updateSchoolDto)
        {
            UpdateSchoolResponseModel updateSchoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateSchoolDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, "/api/v1/School/UpdateSchool", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateSchoolResponseModel = JsonConvert.DeserializeObject<UpdateSchoolResponseModel>(_oApiResponse.data);
                if (updateSchoolResponseModel.Succeeded)
                {
                    updateSchoolResponseModel.Succeeded = true;
                }
                else
                {
                    updateSchoolResponseModel.Succeeded = false;
                }
            }

            return updateSchoolResponseModel;
        }

        public async Task<GetAllSchoolByFiltersResponseModel> GetSchoolByFilter(string baseurl, string SchoolName, string InstituteName, string City, string State, bool IsActive, DateTime CreatedDate)
        {
            GetAllSchoolByFiltersResponseModel getSchoolByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, $"/api/v1/School/GetSchoolByFilter?SchoolName={SchoolName}&InstituteName={InstituteName}&City={City}&State={State}&IsActive={IsActive}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getSchoolByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllSchoolByFiltersResponseModel>(_oApiResponse.data);
                getSchoolByFiltersResponseModel.Succeeded = true;
            }

            return getSchoolByFiltersResponseModel;

        }




        #endregion
    }








}

