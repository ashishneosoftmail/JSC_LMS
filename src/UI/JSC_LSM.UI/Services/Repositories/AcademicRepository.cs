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
    public class AcademicRepository:IAcademicRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public AcademicRepository()
        {

        }

        public async Task<GetAllAcademicListResponseModel> GetAllAcademicDetails()
        {
            GetAllAcademicListResponseModel getAllAcademicListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllAcademicDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAcademicListResponseModel = JsonConvert.DeserializeObject<GetAllAcademicListResponseModel>(_oApiResponse.data);
                getAllAcademicListResponseModel.Succeeded = true;
            }

            return getAllAcademicListResponseModel;
        }

        public async Task<GetAcademicByIdResponseModel> GetAcademicById(int Id)
        {
            GetAcademicByIdResponseModel getAcademicByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAcademicById + $"?id={Id}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAcademicByIdResponseModel = JsonConvert.DeserializeObject<GetAcademicByIdResponseModel>(_oApiResponse.data);
                getAcademicByIdResponseModel.Succeeded = true;
            }

            return getAcademicByIdResponseModel;


        }

        public async Task<GetAllAcademicByFiltersResponseModel> GetAcademicByFilters(string SchoolName, string ClassName, string SectionName, string SubjectName,string TeacherName,string Type, DateTime CreatedDate, bool IsActive)
        {
            GetAllAcademicByFiltersResponseModel getAcademicByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Academic//api/v1/Academic/GetAcademicByFilter?_ClassName={ClassName}&_SubjectName={SubjectName}&_TeacherName={TeacherName}&_Type={Type}&_SchoolName={SchoolName}&_SectionName={SectionName}&_IsActive={IsActive}&_CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAcademicByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllAcademicByFiltersResponseModel>(_oApiResponse.data);
                getAcademicByFiltersResponseModel.Succeeded = true;
            }

            return getAcademicByFiltersResponseModel;

        }

        public async Task<GetAllAcademicByPaginationResponseModel> GetAcademicByPagination(int page, int size)
        {
            GetAllAcademicByPaginationResponseModel getAllAcademicByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllAcademicByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAcademicByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllAcademicByPaginationResponseModel>(_oApiResponse.data);
                getAllAcademicByPaginationResponseModel.Succeeded = true;
            }

            return getAllAcademicByPaginationResponseModel;

        }


    }
}
