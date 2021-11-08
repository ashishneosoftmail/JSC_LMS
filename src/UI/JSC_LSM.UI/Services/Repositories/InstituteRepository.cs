using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute;
using JSC_LMS.Application.Models.Authentication;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
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
    

    public class InstituteRepository : IInstituteRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;

        public InstituteRepository()
        {

        }

        public async Task<InstituteResponseModel> CreateInstitute(CreateInstituteDto createInstituteDto)
        {
            InstituteResponseModel instituteResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createInstituteDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.CreateInstitute, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                instituteResponseModel = JsonConvert.DeserializeObject<InstituteResponseModel>(_oApiResponse.data);
                if (instituteResponseModel.Succeeded)
                {
                    instituteResponseModel.Succeeded = true;
                }
                else
                {
                    instituteResponseModel.Succeeded = false;
                }
            }

            return instituteResponseModel;

        }

        public async Task<GetAllInstituteListResponseModel> GetAllInstituteDetails()
        {
            GetAllInstituteListResponseModel getAllInstituteListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllInstituteDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllInstituteListResponseModel = JsonConvert.DeserializeObject<GetAllInstituteListResponseModel>(_oApiResponse.data);
                getAllInstituteListResponseModel.Succeeded = true;
            }

            return getAllInstituteListResponseModel;

        }

        public async Task<GetInstituteByIdResponseModel> GetInstituteById(int Id)
        {
            GetInstituteByIdResponseModel getInstituteByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetInstituteById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getInstituteByIdResponseModel = JsonConvert.DeserializeObject<GetInstituteByIdResponseModel>(_oApiResponse.data);
                getInstituteByIdResponseModel.Succeeded = true;
            }

            return getInstituteByIdResponseModel;

        }


       public async Task<GetAllInstituteByFiltersResponseModel> GetInstituteByFilters(string InstituteName, string City, string State, DateTime LicenseExpiry, bool IsActive)
        {
            GetAllInstituteByFiltersResponseModel getInstituteByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Institute/GetInstituteByFilter?InstituteName={InstituteName}&City={City}&State={State}&IsActive={IsActive}&LicenseExpiry={LicenseExpiry:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getInstituteByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllInstituteByFiltersResponseModel>(_oApiResponse.data);
                getInstituteByFiltersResponseModel.Succeeded = true;
            }

            return getInstituteByFiltersResponseModel;

        }


        public async Task<GetAllInstituteByPaginationResponseModel> GetInstituteByPagination(int page, int size)
        {
            GetAllInstituteByPaginationResponseModel getAllInstituteByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllInstituteByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllInstituteByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllInstituteByPaginationResponseModel>(_oApiResponse.data);
                getAllInstituteByPaginationResponseModel.Succeeded = true;
            }

            return getAllInstituteByPaginationResponseModel;

        }

        public async Task<UpdateInstituteResponseModel> UpdateInstitute(UpdateInstituteDto updateInstituteDto)
        {
            UpdateInstituteResponseModel updateInstituteResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateInstituteDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.UpdateInstitute, HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateInstituteResponseModel = JsonConvert.DeserializeObject<UpdateInstituteResponseModel>(_oApiResponse.data);
                if (updateInstituteResponseModel.Succeeded)
                {
                    updateInstituteResponseModel.Succeeded = true;
                }
                else
                {
                    updateInstituteResponseModel.Succeeded = false;
                }
            }

            return updateInstituteResponseModel;
        }

    }


} 
