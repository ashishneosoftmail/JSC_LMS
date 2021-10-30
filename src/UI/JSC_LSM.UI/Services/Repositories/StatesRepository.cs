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
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.Repositories
{
   
    public class StatesRepository : IStateRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public StatesRepository()
        {

        }
        public async Task<GetAllStatesResponseModel> GetAllStates()
        {
            GetAllStatesResponseModel getAllStatesResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllState, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllStatesResponseModel = JsonConvert.DeserializeObject<GetAllStatesResponseModel>(_oApiResponse.data);
                getAllStatesResponseModel.isSuccess = true;
            }

            return getAllStatesResponseModel;

        }

    }
}
