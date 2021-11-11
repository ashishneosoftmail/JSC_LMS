

using JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher;
using JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher;
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

        public async Task<GetTeacherByIdResponseModel> GetTeacherById(int Id)
        {
            GetTeacherByIdResponseModel getTeacherByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetTeacherById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getTeacherByIdResponseModel = JsonConvert.DeserializeObject<GetTeacherByIdResponseModel>(_oApiResponse.data);
                getTeacherByIdResponseModel.Succeeded = true;
            }

            return getTeacherByIdResponseModel;

        }

        public async Task<UpdateTeacherResponseModel> UpdateTeacher(UpdateTeacherDto updateTeacherDto)
        {
            UpdateTeacherResponseModel updateTeacherResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateTeacherDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication("/api/v1/Teacher/UpdateTeacher", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateTeacherResponseModel = JsonConvert.DeserializeObject<UpdateTeacherResponseModel>(_oApiResponse.data);
                if (updateTeacherResponseModel.Succeeded)
                {
                    updateTeacherResponseModel.Succeeded = true;
                }
                else
                {
                    updateTeacherResponseModel.Succeeded = false;
                }
            }

            return updateTeacherResponseModel;
        }

    }
}
