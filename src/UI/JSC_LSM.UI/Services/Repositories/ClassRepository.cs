using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Class.Commands.UpdateClass;
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
    public class ClassRepository : IClassRepository
    {

        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public ClassRepository()
        {

        }


        public async Task<GetAllClassResponseModel> GetAllClass(string baseurl)
        {
            GetAllClassResponseModel getAllClassResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllClass, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllClassResponseModel = JsonConvert.DeserializeObject<GetAllClassResponseModel>(_oApiResponse.data);
                getAllClassResponseModel.isSuccess = true;
            }

            return getAllClassResponseModel;

        }


        public async Task<GetAllClassListResponseModel> GetAllClassDetails(string baseurl)
        {
            GetAllClassListResponseModel getAllClassListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllClassDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllClassListResponseModel = JsonConvert.DeserializeObject<GetAllClassListResponseModel>(_oApiResponse.data);
                getAllClassListResponseModel.Succeeded = true;
            }

            return getAllClassListResponseModel;
        }

            public async Task<ClassResponseModel> AddNewClass(string baseurl, CreateClassDto createClassDto)
        {
            ClassResponseModel classResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createClassDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddNewClass, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                classResponseModel = JsonConvert.DeserializeObject<ClassResponseModel>(_oApiResponse.data);
                if (classResponseModel.Succeeded)
                {
                    classResponseModel.Succeeded = true;
                }
                else
                {
                    classResponseModel.Succeeded = false;
                }
            }

            return classResponseModel;

        }

    

        public async Task<GetClassByIdResponseModel> GetClassById(string baseurl, int Id)
        {
            GetClassByIdResponseModel getClassByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetClassById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getClassByIdResponseModel = JsonConvert.DeserializeObject<GetClassByIdResponseModel>(_oApiResponse.data);
                getClassByIdResponseModel.Succeeded = true;
            }

            return getClassByIdResponseModel;


        }

        public async Task<GetAllClassByFiltersResponseModel> GetClassByFilters(string baseurl, string SchoolName, string ClassName, DateTime CreatedDate, bool IsActive)
        {
            GetAllClassByFiltersResponseModel getClassByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, $"/api/v1/Class/GetClassByFilter?SchoolName={SchoolName}&ClassName={ClassName}&IsActive={IsActive}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getClassByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllClassByFiltersResponseModel>(_oApiResponse.data);
                getClassByFiltersResponseModel.Succeeded = true;
            }

            return getClassByFiltersResponseModel;

        }

        public async Task<GetAllClassByPaginationResponseModel> GetClassByPagination(string baseurl, int page, int size)
        {
            GetAllClassByPaginationResponseModel getAllClassByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllClassByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllClassByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllClassByPaginationResponseModel>(_oApiResponse.data);
                getAllClassByPaginationResponseModel.Succeeded = true;
            }

            return getAllClassByPaginationResponseModel;

        }

        public async Task<UpdateClassResponseModel> UpdateClass(string baseurl, UpdateClassDto updateClassDto)
        {
            UpdateClassResponseModel updateClassResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateClassDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, "/api/v1/Class/UpdateClass", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateClassResponseModel = JsonConvert.DeserializeObject<UpdateClassResponseModel>(_oApiResponse.data);
                if (updateClassResponseModel.Succeeded)
                {
                    updateClassResponseModel.Succeeded = true;
                }
                else
                {
                    updateClassResponseModel.Succeeded = false;
                }
            }

            return updateClassResponseModel;
        }


    }
    }