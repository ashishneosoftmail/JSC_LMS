using JSC_LMS.Application.Features.Section.Commands.CreateSection;
using JSC_LMS.Application.Features.Section.Commands.CreateUpdate;
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


        public async Task<GetAllSectionListResponseModel> GetAllSectionDetails()
        {
            GetAllSectionListResponseModel getAllSectionListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllSectionDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllSectionListResponseModel = JsonConvert.DeserializeObject<GetAllSectionListResponseModel>(_oApiResponse.data);
                getAllSectionListResponseModel.Succeeded = true;
            }

            return getAllSectionListResponseModel;
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

        public async Task<GetSectionByIdResponseModel> GetSectionById(int Id)
        {
            GetSectionByIdResponseModel getSectionByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetSectionById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getSectionByIdResponseModel = JsonConvert.DeserializeObject<GetSectionByIdResponseModel>(_oApiResponse.data);
                getSectionByIdResponseModel.Succeeded = true;
            }

            return getSectionByIdResponseModel;


        }


        public async Task<GetAllSectionByFiltersResponseModel> GetSectionByFilters(string SchoolName, string ClassName, string SectionName, DateTime CreatedDate, bool IsActive)
        {
            GetAllSectionByFiltersResponseModel getSectionByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Section/GetSectionByFilter?SchoolName={SchoolName}&ClassName={ClassName}&SectionName={SectionName}&IsActive={IsActive}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getSectionByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllSectionByFiltersResponseModel>(_oApiResponse.data);
                getSectionByFiltersResponseModel.Succeeded = true;
            }

            return getSectionByFiltersResponseModel;

        }

        public async Task<GetAllSectionByPaginationResponseModel> GetSectionByPagination(int page, int size)
        {
            GetAllSectionByPaginationResponseModel getAllSectionByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllSectionByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllSectionByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllSectionByPaginationResponseModel>(_oApiResponse.data);
                getAllSectionByPaginationResponseModel.Succeeded = true;
            }

            return getAllSectionByPaginationResponseModel;

        }


        public async Task<UpdateSectionResponseModel> UpdateSection(UpdateSectionDto updateSectionDto)
        {
            UpdateSectionResponseModel updateSectionResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateSectionDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication("/api/v1/Section/UpdateSection", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateSectionResponseModel = JsonConvert.DeserializeObject<UpdateSectionResponseModel>(_oApiResponse.data);
                if (updateSectionResponseModel.Succeeded)
                {
                    updateSectionResponseModel.Succeeded = true;
                }
                else
                {
                    updateSectionResponseModel.Succeeded = false;
                }
            }

            return updateSectionResponseModel;
        }




    }
}
