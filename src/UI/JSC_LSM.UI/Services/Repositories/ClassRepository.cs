using JSC_LMS.Application.Features.Class.Commands.CreateClass;
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
    public class ClassRepository : IClassRepository
    {

        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public ClassRepository()
        {

        }
        public async Task<ClassResponseModel> AddNewClass(CreateClassDto createClassDto)
        {
            ClassResponseModel classResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createClassDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddNewClass, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                classResponseModel = JsonConvert.DeserializeObject<ClassResponseModel>(_oApiResponse.data);
                if (classResponseModel.Succeeded)
                {
                    classResponseModel.Succeeded = true;
                }
                else
                {
                    classResponseModel.Succeeded = false;
                }
            }

            return classResponseModel;

        }

        public async Task<GetAllClassListResponseModel> GetAllClassDetails()
        {
            GetAllClassListResponseModel getAllClassListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllClassDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllClassListResponseModel = JsonConvert.DeserializeObject<GetAllClassListResponseModel>(_oApiResponse.data);
                getAllClassListResponseModel.Succeeded = true;
            }

            return getAllClassListResponseModel;

        }

    }
}