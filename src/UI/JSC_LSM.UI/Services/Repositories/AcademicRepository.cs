using JSC_LMS.Application.Features.Academics.Commands.CreateAcademic;
using JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic;
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

        public async Task<GetAllAcademicListResponseModel> GetAllAcademicDetails(string baseurl)
        {
            GetAllAcademicListResponseModel getAllAcademicListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllAcademicDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAcademicListResponseModel = JsonConvert.DeserializeObject<GetAllAcademicListResponseModel>(_oApiResponse.data);
                getAllAcademicListResponseModel.Succeeded = true;
            }

            return getAllAcademicListResponseModel;
        }

        public async Task<GetAcademicByIdResponseModel> GetAcademicById(string baseurl, int Id)
        {
            GetAcademicByIdResponseModel getAcademicByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAcademicById + $"?id={Id}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAcademicByIdResponseModel = JsonConvert.DeserializeObject<GetAcademicByIdResponseModel>(_oApiResponse.data);
                getAcademicByIdResponseModel.Succeeded = true;
            }

            return getAcademicByIdResponseModel;


        }

        public async Task<GetAllAcademicByFiltersResponseModel> GetAcademicByFilters(string baseurl, string SchoolName, string ClassName, string SectionName, string SubjectName,string TeacherName,string Type, DateTime CreatedDate, bool IsActive)
        {
            GetAllAcademicByFiltersResponseModel getAcademicByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, $"/api/v1/Academic/GetAcademicByFilter?ClassName={ClassName}&SchoolName={SchoolName}&SubjectName={SubjectName}&SectionName={SectionName}&TeacherName={TeacherName}&Type={Type}&IsActive={IsActive}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAcademicByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllAcademicByFiltersResponseModel>(_oApiResponse.data);
                getAcademicByFiltersResponseModel.Succeeded = true;
            }

            return getAcademicByFiltersResponseModel;

        }

        public async Task<GetAllAcademicByPaginationResponseModel> GetAcademicByPagination( string baseurl, int page, int size)
        {
            GetAllAcademicByPaginationResponseModel getAllAcademicByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllAcademicByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllAcademicByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllAcademicByPaginationResponseModel>(_oApiResponse.data);
                getAllAcademicByPaginationResponseModel.Succeeded = true;
            }

            return getAllAcademicByPaginationResponseModel;

        }


        public async Task<UpdateAcademicResponseModel> UpdateAcademic(string baseurl, UpdateAcademicDto updateAcademicDto)
        {
            UpdateAcademicResponseModel updateAcademicResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateAcademicDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, "/api/v1/Academic/UpdateAcademic", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateAcademicResponseModel = JsonConvert.DeserializeObject<UpdateAcademicResponseModel>(_oApiResponse.data);
                if (updateAcademicResponseModel.Succeeded)
                {
                    updateAcademicResponseModel.Succeeded = true;
                }
                else
                {
                    updateAcademicResponseModel.Succeeded = false;
                }
            }

            return updateAcademicResponseModel;
        }

        public async Task<AcademicResponseModel> AddNewAcademic(string baseurl, CreateAcademicDto createAcademicDto)
        {
            AcademicResponseModel academicResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createAcademicDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddNewAcademic, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                academicResponseModel = JsonConvert.DeserializeObject<AcademicResponseModel>(_oApiResponse.data);
                if (academicResponseModel.Succeeded)
                {
                    academicResponseModel.Succeeded = true;
                }
                else
                {
                    academicResponseModel.Succeeded = false;
                }
            }

            return academicResponseModel;

        }


    }
}
