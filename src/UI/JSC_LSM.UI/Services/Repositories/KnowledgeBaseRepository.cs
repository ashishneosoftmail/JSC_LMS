using JSC_LMS.Application.Features.KnowledgeBase.Commands.CreateKnowledgeBase;
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
    public class KnowledgeBaseRepository : IKnowledgeBaseRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public KnowledgeBaseRepository()
        {

        }
        public async Task<AddKnowledgeBaseResponseModel> AddKnowledgeBase(CreateKnowledgeBaseDto createKnowledgeBaseDto)
        {
            AddKnowledgeBaseResponseModel aAddKnowledgeBaseResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createKnowledgeBaseDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddKnowledgeBase, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                aAddKnowledgeBaseResponseModel = JsonConvert.DeserializeObject<AddKnowledgeBaseResponseModel>(_oApiResponse.data);
                if (aAddKnowledgeBaseResponseModel.Succeeded)
                {
                    aAddKnowledgeBaseResponseModel.Succeeded = true;
                }
                else
                {
                    aAddKnowledgeBaseResponseModel.Succeeded = false;
                }
            }

            return aAddKnowledgeBaseResponseModel;
        }
    }
}
