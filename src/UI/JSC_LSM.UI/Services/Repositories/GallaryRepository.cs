using JSC_LMS.Application.Features.Gallary.Commands.UploadImage;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels.GallaryResponseModel;
using JSC_LSM.UI.Services.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.Repositories
{
    public class GallaryRepository:IGallaryRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly Microsoft.Extensions.Options.IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public GallaryRepository()
        {

        }

        public async Task<AddGallaryResponseModel> AddGallary(string baseurl, UploadImageDto uploadImageDto)
        {
            AddGallaryResponseModel addGallaryResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(uploadImageDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddGallary, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                addGallaryResponseModel = JsonConvert.DeserializeObject<AddGallaryResponseModel>(_oApiResponse.data);
                if (addGallaryResponseModel.Succeeded)
                {
                    addGallaryResponseModel.Succeeded = true;
                }
                else
                {
                    addGallaryResponseModel.Succeeded = false;
                }
            }

            return addGallaryResponseModel;
        }

        public async Task<GetAllGallaryListResponseModel> GetGallaryList(string baseurl)
        {
            GetAllGallaryListResponseModel getAllGallaryListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllGallaryList, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllGallaryListResponseModel = JsonConvert.DeserializeObject<GetAllGallaryListResponseModel>(_oApiResponse.data);
                getAllGallaryListResponseModel.Succeeded = true;
            }

            return getAllGallaryListResponseModel;
        }

        public async Task<GetGallaryListByIdResponseModel> GetGallaryById(string baseurl, int Id)
        {
            GetGallaryListByIdResponseModel getGallaryListByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetGallaryById + "?id="+ Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getGallaryListByIdResponseModel = JsonConvert.DeserializeObject<GetGallaryListByIdResponseModel>(_oApiResponse.data);
                getGallaryListByIdResponseModel.Succeeded = true;
            }

            return getGallaryListByIdResponseModel;
        }

        public async Task DeleteGallary(string baseurl, int id)
        {

            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.DeleteGallaryById + "?id=" + id, HttpMethod.Delete, bytes, _sToken);
        }

        public async Task DeleteAllGallary(string baseurl)
        {

            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.DeleteAllGallary, HttpMethod.Delete, bytes, _sToken);
        }

        public async Task<GetGallaryListBySchoolIdResponseModel> GetGallaryBySchoolId(string baseurl, int SchoolId)
        {
            GetGallaryListBySchoolIdResponseModel getGallaryListByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetGallaryBySchoolId +$"?SchoolId={SchoolId}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getGallaryListByIdResponseModel = JsonConvert.DeserializeObject<GetGallaryListBySchoolIdResponseModel>(_oApiResponse.data);
                getGallaryListByIdResponseModel.Succeeded = true;
            }

            return getGallaryListByIdResponseModel;
        }



    }
}
