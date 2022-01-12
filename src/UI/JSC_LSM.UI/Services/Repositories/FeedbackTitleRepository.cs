using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.Repositories
{
    public class FeedbackTitleRepository : IFeedbackTitleRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public FeedbackTitleRepository()
        {

        }

        public async Task<GetFeedbackTitleListResponseModel> GetFeedbackTitleDetails(string baseurl)
        {
            GetFeedbackTitleListResponseModel getAllFeedbackListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(baseurl, UrlHelper.GetFeedbackTitleDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllFeedbackListResponseModel = JsonConvert.DeserializeObject<GetFeedbackTitleListResponseModel>(_oApiResponse.data);
                getAllFeedbackListResponseModel.Succeeded = true;
            }

            return getAllFeedbackListResponseModel;
        }

    }
}
