using JSC_LMS.Application.Features.Circulars.Commands.CreateCircular;
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
    public class CircularRepository : ICircularRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public CircularRepository()
        {

        }

        public async Task<AddCircularResponseModel> AddCircular(CreateCircularDto createCircularDto)
        {
            AddCircularResponseModel addCircularResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createCircularDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddCircular, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                addCircularResponseModel = JsonConvert.DeserializeObject<AddCircularResponseModel>(_oApiResponse.data);
                if (addCircularResponseModel.Succeeded)
                {
                    addCircularResponseModel.Succeeded = true;
                }
                else
                {
                    addCircularResponseModel.Succeeded = false;
                }
            }

            return addCircularResponseModel;
        }

        public async Task<GetAllCircularListByPaginationResponseModel> GetAllCircularListByPagination(int page, int size)
        {
            GetAllCircularListByPaginationResponseModel getAllCircularListByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllCircularByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularListByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllCircularListByPaginationResponseModel>(_oApiResponse.data);
                getAllCircularListByPaginationResponseModel.Succeeded = true;
            }

            return getAllCircularListByPaginationResponseModel;
        }

        public async Task<GetAllCircularListResponseModel> GetAllCircularList()
        {
            GetAllCircularListResponseModel getAllCircularListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllCircularList, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllCircularListResponseModel = JsonConvert.DeserializeObject<GetAllCircularListResponseModel>(_oApiResponse.data);
                getAllCircularListResponseModel.Succeeded = true;
            }

            return getAllCircularListResponseModel;
        }
    }
}
