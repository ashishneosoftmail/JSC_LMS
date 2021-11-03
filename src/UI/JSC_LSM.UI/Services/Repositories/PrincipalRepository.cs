using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.Repositories
{
    public class PrincipalRepository : IPrincipalRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public PrincipalRepository()
        {

        }
        public async Task<GetAllPrincipalListResponseModel> GetAllPrincipalDetails()
        {
            GetAllPrincipalListResponseModel getAllPrincipalListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllPrincipalDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllPrincipalListResponseModel = JsonConvert.DeserializeObject<GetAllPrincipalListResponseModel>(_oApiResponse.data);
                getAllPrincipalListResponseModel.Succeeded = true;
            }

            return getAllPrincipalListResponseModel;

        }
        public async Task<GetPrincipalByIdResponseModel> GetPrincipalById(int Id)
        {
            GetPrincipalByIdResponseModel getPrincipalByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetPrincipalById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getPrincipalByIdResponseModel = JsonConvert.DeserializeObject<GetPrincipalByIdResponseModel>(_oApiResponse.data);
                getPrincipalByIdResponseModel.Succeeded = true;
            }

            return getPrincipalByIdResponseModel;

        }

        public async Task<GetAllPrincipalByFiltersResponseModel> GetPrincipalByFilters(string SchoolName, string PrincipalName, DateTime CreatedDate, bool IsActive)
        {
            GetAllPrincipalByFiltersResponseModel getPrincipalByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Principal/GetPrincipalByFilter?SchoolName={SchoolName}&PrincipalName={PrincipalName}&IsActive={IsActive}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getPrincipalByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllPrincipalByFiltersResponseModel>(_oApiResponse.data);
                getPrincipalByFiltersResponseModel.Succeeded = true;
            }

            return getPrincipalByFiltersResponseModel;

        }
    }

}
