﻿using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
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

#region -developed by Shivani Goswami
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

        /// <summary>
        /// Add new institute : by Shivani Goswami
        /// </summary>
        /// <param name="createInstituteDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns all the institute data : by Shivani Goswami
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the institute data for a particular Id : by Shivani Goswami
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the institute data based on the search parameters : by Shivani Goswami
        /// </summary>
        /// <param name="InstituteName"></param>
        /// <param name="City"></param>
        /// <param name="State"></param>
        /// <param name="LicenseExpiry"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the institute Data based on the page no and no of rows per page : by Shivani Goswami
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the institute Data : by Shivani Goswami
        /// </summary>
        /// <param name="updateInstituteDto"></param>
        /// <returns></returns>
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
#endregion