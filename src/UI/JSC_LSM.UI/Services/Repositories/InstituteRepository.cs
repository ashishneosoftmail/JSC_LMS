﻿using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LMS.Application.Models.Authentication;
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
                if (instituteResponseModel.isSuccess)
                {
                    instituteResponseModel.isSuccess = true;
                }
                else
                {
                    instituteResponseModel.isSuccess = false;
                }
            }

            return instituteResponseModel;

        }
    }


} 
