using JSC_LMS.Application.Features.Subject.Commands.CreateSubject;
using JSC_LMS.Application.Features.Subject.Commands.UpdateSubject;
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
    public class SubjectRepository:ISubjectRepository
    {

        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public SubjectRepository()
        {

        }

        public async Task<GetAllSubjectListResponseModel> GetAllSubjectDetails()
        {
            GetAllSubjectListResponseModel getAllSubjectListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllSubjectDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllSubjectListResponseModel = JsonConvert.DeserializeObject<GetAllSubjectListResponseModel>(_oApiResponse.data);
                getAllSubjectListResponseModel.Succeeded = true;
            }

            return getAllSubjectListResponseModel;
        }

        public async Task<SubjectResponseModel> AddNewSubject(CreateSubjectDto createSubjectDto)
        {
            SubjectResponseModel subjectResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createSubjectDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddNewSubject, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                subjectResponseModel = JsonConvert.DeserializeObject<SubjectResponseModel>(_oApiResponse.data);
                if (subjectResponseModel.Succeeded)
                {
                    subjectResponseModel.Succeeded = true;
                }
                else
                {
                    subjectResponseModel.Succeeded = false;
                }
            }

            return subjectResponseModel;

        }

        public async Task<GetSubjectByIdResponseModel> GetSubjectById(int Id)
        {
            GetSubjectByIdResponseModel getSubjectByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetSubjectById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getSubjectByIdResponseModel = JsonConvert.DeserializeObject<GetSubjectByIdResponseModel>(_oApiResponse.data);
                getSubjectByIdResponseModel.Succeeded = true;
            }

            return getSubjectByIdResponseModel;


        }

        public async Task<GetAllSubjectByFiltersResponseModel> GetSubjectByFilters(string SchoolName, string ClassName, string SectionName, string SubjectName, DateTime CreatedDate, bool IsActive)
        {
            GetAllSubjectByFiltersResponseModel getSubjectByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Subject/Filter?_ClassName={ClassName}&_SubjectName={SubjectName}&_SchoolName={SchoolName}&_SectionName={SectionName}&_IsActive={IsActive}&_CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getSubjectByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllSubjectByFiltersResponseModel>(_oApiResponse.data);
                getSubjectByFiltersResponseModel.Succeeded = true;
            }

            return getSubjectByFiltersResponseModel;

        }

        public async Task<GetAllSubjectByPaginationResponseModel> GetSubjectByPagination(int page, int size)
        {
            GetAllSubjectByPaginationResponseModel getAllSubjectByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetSubjectByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllSubjectByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllSubjectByPaginationResponseModel>(_oApiResponse.data);
                getAllSubjectByPaginationResponseModel.Succeeded = true;
            }

            return getAllSubjectByPaginationResponseModel;

        }


        public async Task<UpdateSubjectResponseModel> UpdateSubject(UpdateSubjectDto updateSubjectDto)
        {
            UpdateSubjectResponseModel updateSubjectResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateSubjectDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication("/api/v1/Subject/UpdateSubject", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateSubjectResponseModel = JsonConvert.DeserializeObject<UpdateSubjectResponseModel>(_oApiResponse.data);
                if (updateSubjectResponseModel.Succeeded)
                {
                    updateSubjectResponseModel.Succeeded = true;
                }
                else
                {
                    updateSubjectResponseModel.Succeeded = false;
                }
            }

            return updateSubjectResponseModel;
        }


    }
}
