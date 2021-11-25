using JSC_LMS.Application.Features.Students.Commands.CreateStudent;
using JSC_LMS.Application.Features.Students.Commands.UpdateStudent;
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
    public class StudentRepository : IStudentRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public StudentRepository()
        {

        }

        public async Task<StudentResponseModel> AddNewStudent(CreateStudentDto createStudentDto)
        {
            StudentResponseModel studentResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createStudentDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddNewStudent, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                studentResponseModel = JsonConvert.DeserializeObject<StudentResponseModel>(_oApiResponse.data);
                if (studentResponseModel.Succeeded)
                {
                    studentResponseModel.Succeeded = true;
                }
                else
                {
                    studentResponseModel.Succeeded = false;
                }
            }

            return studentResponseModel;

        }

        public async Task<GetAllStudentListResponseModel> GetAllStudentDetails()
        {
            GetAllStudentListResponseModel getAllStudentListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllStudentDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllStudentListResponseModel = JsonConvert.DeserializeObject<GetAllStudentListResponseModel>(_oApiResponse.data);
                getAllStudentListResponseModel.Succeeded = true;
            }

            return getAllStudentListResponseModel;

        }

        public async Task<GetStudentByIdResponseModel> GetStudentById(int Id)
        {
            GetStudentByIdResponseModel getStudentByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetStudentById+"?id=" + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getStudentByIdResponseModel = JsonConvert.DeserializeObject<GetStudentByIdResponseModel>(_oApiResponse.data);
                getStudentByIdResponseModel.Succeeded = true;
            }

            return getStudentByIdResponseModel;

        }

        public async Task<GetAllStudentByFiltersResponseModel> GetStudentByFilters(string ClassName, string SectionName,string StudentName, bool IsActive, DateTime CreatedDate)
        {
            GetAllStudentByFiltersResponseModel getStudentByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Student/GetStudentByFilter?ClassName={ClassName}&SectionName={SectionName}&StudentName={StudentName}&IsActive={IsActive}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getStudentByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllStudentByFiltersResponseModel>(_oApiResponse.data);
                getStudentByFiltersResponseModel.Succeeded = true;
            }

            return getStudentByFiltersResponseModel;

        }

        public async Task<GetAllStudentByPaginationResponseModel> GetStudentByPagination(int page, int size)
        {
            GetAllStudentByPaginationResponseModel getAllStudentByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllStudentByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllStudentByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllStudentByPaginationResponseModel>(_oApiResponse.data);
                getAllStudentByPaginationResponseModel.Succeeded = true;
            }

            return getAllStudentByPaginationResponseModel;

        }

        public async Task<UpdateStudentResponseModel> UpdateStudent(UpdateStudentDto updateStudentDto)
        {
            UpdateStudentResponseModel updateStudentResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateStudentDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication("/api/v1/Student/Update", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateStudentResponseModel = JsonConvert.DeserializeObject<UpdateStudentResponseModel>(_oApiResponse.data);
                if (updateStudentResponseModel.Succeeded)
                {
                    updateStudentResponseModel.Succeeded = true;
                }
                else
                {
                    updateStudentResponseModel.Succeeded = false;
                }
            }

            return updateStudentResponseModel;
        }





        public async Task<GetStudentByUserIdResponseModel> GetStudentByUserId(string UserId)
        {
            GetStudentByUserIdResponseModel getStudentByUserIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetStudentByUserId + $"?UserId={UserId}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getStudentByUserIdResponseModel = JsonConvert.DeserializeObject<GetStudentByUserIdResponseModel>(_oApiResponse.data);
                getStudentByUserIdResponseModel.Succeeded = true;
            }

            return getStudentByUserIdResponseModel;
        }

    }
}
