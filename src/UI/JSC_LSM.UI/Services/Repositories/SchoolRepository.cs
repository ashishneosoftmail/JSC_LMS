using JSC_LMS.Application.Features.Class.Commands.CreateClass;
using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
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
        public async Task<GetAllSchoolResponseModel> GetAllSchool()
        {
            GetAllSchoolResponseModel getAllSchoolResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllSchool, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllSchoolResponseModel = JsonConvert.DeserializeObject<GetAllSchoolResponseModel>(_oApiResponse.data);
                getAllSchoolResponseModel.isSuccess = true;
            }

            return getAllSchoolResponseModel;

        }
        public async Task<PrincipalResponseModel> AddNewPrinicipal(CreatePrincipalDto createPrincipalDto)
        {
            PrincipalResponseModel principalResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createPrincipalDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddNewPrincipal, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                principalResponseModel = JsonConvert.DeserializeObject<PrincipalResponseModel>(_oApiResponse.data);
                if (principalResponseModel.Succeeded)
                {
                    principalResponseModel.Succeeded = true;
                }
                else
                {
                    principalResponseModel.Succeeded = false;
                }
            }

            return principalResponseModel;

        }

    

        }

    }

