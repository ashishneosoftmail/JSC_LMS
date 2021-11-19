
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Helpers;
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
using JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents;

namespace JSC_LSM.UI.Services.Repositories
{
    public class ParentsRepository : IParentsRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public ParentsRepository()
        {

        }

        public async Task<ParentsResponseModel> AddNewParents(CreateParentsDto createParentsDto)
        {
            ParentsResponseModel parentsResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createParentsDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddNewParents, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                parentsResponseModel = JsonConvert.DeserializeObject<ParentsResponseModel>(_oApiResponse.data);
                if (parentsResponseModel.Succeeded)
                {
                    parentsResponseModel.Succeeded = true;
                }
                else
                {
                    parentsResponseModel.Succeeded = false;
                }
            }

            return parentsResponseModel;

        }
    }
}
