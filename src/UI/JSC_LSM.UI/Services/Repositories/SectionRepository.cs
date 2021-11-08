using JSC_LMS.Application.Features.Section.Commands.CreateSection;
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
    public class SectionRepository : ISectionRepository
    {
     
            private APIRepository _aPIRepository;
            private APICommunicationResponseModel<string> _oApiResponse;
            private string _sToken = string.Empty;
            private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
            private readonly IConfiguration _configuration;
            public SectionRepository()
            {

            }

        public async Task<SectionResponseModel> AddNewSection(CreateSectionDto createSectionDto)
        {
            SectionResponseModel sectionResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createSectionDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddNewSection, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                sectionResponseModel = JsonConvert.DeserializeObject<SectionResponseModel>(_oApiResponse.data);
                if (sectionResponseModel.Succeeded)
                {
                    sectionResponseModel.Succeeded = true;
                }
                else
                {
                    sectionResponseModel.Succeeded = false;
                }
            }

            return sectionResponseModel;

        }


    }
}
