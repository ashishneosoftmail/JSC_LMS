

using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
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
    public class TeacherRepository : ITeacherRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;

        public TeacherRepository()
        {

        }

        public async Task<TeacherResponseModel> CreateTeacher(CreateTeacherDto createTeacherDto)
        {
            TeacherResponseModel teacherResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createTeacherDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.CreateTeacher, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                teacherResponseModel = JsonConvert.DeserializeObject<TeacherResponseModel>(_oApiResponse.data);
                if (teacherResponseModel.Succeeded)
                {
                    teacherResponseModel.Succeeded = true;
                }
                else
                {
                    teacherResponseModel.Succeeded = false;
                }
            }

            return teacherResponseModel;

        }
       

    }
}
