using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
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
    }

}
