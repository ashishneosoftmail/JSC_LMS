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

    public class CitiesRepository : ICityRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public CitiesRepository()
        {

        }
        public async Task<GetAllCitiesResponseModel> GetAllCities()
        {
            GetAllCitiesResponseModel getAllCitiesResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllCity, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCitiesResponseModel = JsonConvert.DeserializeObject<GetAllCitiesResponseModel>(_oApiResponse.data);
                getAllCitiesResponseModel.isSuccess = true;
            }

            return getAllCitiesResponseModel;

        }

    }
}