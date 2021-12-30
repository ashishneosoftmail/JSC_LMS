using JSC_LMS.Application.Features.Common.Categories.Commands;
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
    public class CategoryRepository : ICategoryRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public CategoryRepository()
        {

        }

        public async Task<CreateCategoryResponseModel> AddCategory(string baseurl, CreateCategoryDto createCategoryDto)
        {
            CreateCategoryResponseModel createCategoryResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddCategory + $"?categoryName={createCategoryDto.CategoryName}&isActive={createCategoryDto.IsActive}", HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                createCategoryResponseModel = JsonConvert.DeserializeObject<CreateCategoryResponseModel>(_oApiResponse.data);
                if (createCategoryResponseModel.Succeeded)
                {
                    createCategoryResponseModel.Succeeded = true;
                }
                else
                {
                    createCategoryResponseModel.Succeeded = false;
                }
            }

            return createCategoryResponseModel;
        }

        public async Task<GetAllCategoryResponseModel> GetAllCategory(string baseurl)
        {
            GetAllCategoryResponseModel getAllCategoryResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllCategory, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCategoryResponseModel = JsonConvert.DeserializeObject<GetAllCategoryResponseModel>(_oApiResponse.data);
                getAllCategoryResponseModel.isSuccess = true;
            }

            return getAllCategoryResponseModel;
        }
    }
}
