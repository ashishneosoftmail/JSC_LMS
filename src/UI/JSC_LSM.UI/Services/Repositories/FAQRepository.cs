using JSC_LMS.Application.Features.FAQ.Commands.CreateFAQ;
using JSC_LMS.Application.Features.FAQ.Commands.UpdateFAQ;
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
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace JSC_LSM.UI.Services.Repositories
{
    public class FAQRepository : IFAQRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public FAQRepository()
        {

        }
        public async Task<AddFAQBaseResponseModel> AddFAQ(string baseurl, CreateFAQDto createFAQDto)
        {
            AddFAQBaseResponseModel aAddFAQResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createFAQDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddFAQ, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                aAddFAQResponseModel = JsonConvert.DeserializeObject<AddFAQBaseResponseModel>(_oApiResponse.data);
                if (aAddFAQResponseModel.Succeeded)
                {
                    aAddFAQResponseModel.Succeeded = true;
                }
                else
                {
                    aAddFAQResponseModel.Succeeded = false;
                }
            }

            return aAddFAQResponseModel;
        }

        public async Task<UpdateFAQResponseModel> EditFAQ(string baseurl, UpdateFAQDto updateFAQDto)
        {
            UpdateFAQResponseModel updateFAQResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateFAQDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, "/api/v1/FAQ/UpdateFAQ", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateFAQResponseModel = JsonConvert.DeserializeObject<UpdateFAQResponseModel>(_oApiResponse.data);
                if (updateFAQResponseModel.Succeeded)
                {
                    updateFAQResponseModel.Succeeded = true;
                }
                else
                {
                    updateFAQResponseModel.Succeeded = false;
                }
            }

            return updateFAQResponseModel;
        }

        public async Task<GetFAQByIdResponseModel> GetFAQById(string baseurl, int id)
        {
            GetFAQByIdResponseModel getFAQByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetFAQById + id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getFAQByIdResponseModel = JsonConvert.DeserializeObject<GetFAQByIdResponseModel>(_oApiResponse.data);
                getFAQByIdResponseModel.Succeeded = true;
            }

            return getFAQByIdResponseModel;
        }

        public async Task<GetAllFAQPaginationResponseModel> GetAllFAQByPagination(string baseurl, int page, int size)
        {
            GetAllFAQPaginationResponseModel getAllFAQPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllFAQByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllFAQPaginationResponseModel = JsonConvert.DeserializeObject<GetAllFAQPaginationResponseModel>(_oApiResponse.data);
                getAllFAQPaginationResponseModel.Succeeded = true;
            }

            return getAllFAQPaginationResponseModel;
        }
        public async Task<GetAllFAQFiltersResponseModel> GetAllFAQByFilters(string baseurl, string faqtitle, bool isactive, string category)
        {
            GetAllFAQFiltersResponseModel getAllFAQFilterResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllFAQByFilters + $"?_FAQTitle={faqtitle}&_IsActive={isactive}&_Category={category}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllFAQFilterResponseModel = JsonConvert.DeserializeObject<GetAllFAQFiltersResponseModel>(_oApiResponse.data);
                getAllFAQFilterResponseModel.Succeeded = true;
            }

            return getAllFAQFilterResponseModel;
        }

        public async Task<GetAllFAQListResponseModel> GetAllFAQList(string baseurl)
        {
            GetAllFAQListResponseModel getAllFAQListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllFAQ, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllFAQListResponseModel = JsonConvert.DeserializeObject<GetAllFAQListResponseModel>(_oApiResponse.data);
                getAllFAQListResponseModel.Succeeded = true;
            }

            return getAllFAQListResponseModel;
        }

        public async Task DeleteFAQ(string baseurl, int id)
        {

            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.DeleteFAQ + id, HttpMethod.Delete, bytes, _sToken);
        }

      
    }
}
