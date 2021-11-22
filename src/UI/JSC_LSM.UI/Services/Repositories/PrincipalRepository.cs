using JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal;
using JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.Models;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#region - Developed By Harsh Chheda
namespace JSC_LSM.UI.Services.Repositories
{
    public class PrincipalRepository : IPrincipalRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public PrincipalRepository()
        {

        }
        /// <summary>
        /// Add New Principal - Developed By Harsh Chheda
        /// </summary>
        /// <param name="createPrincipalDto"></param>
        /// <returns></returns>
        public async Task<PrincipalResponseModel> AddNewPrinicipal(CreatePrincipalDto createPrincipalDto)
        {
            PrincipalResponseModel principalResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createPrincipalDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.AddNewPrincipal, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                principalResponseModel = JsonConvert.DeserializeObject<PrincipalResponseModel>(_oApiResponse.data);
                if (principalResponseModel.Succeeded)
                {
                    principalResponseModel.Succeeded = true;
                }
                else
                {
                    principalResponseModel.Succeeded = false;
                }
            }

            return principalResponseModel;

        }
        /// <summary>
        /// Returns all the principal data - Developed By Harsh Chheda
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllPrincipalListResponseModel> GetAllPrincipalDetails()
        {
            GetAllPrincipalListResponseModel getAllPrincipalListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllPrincipalDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllPrincipalListResponseModel = JsonConvert.DeserializeObject<GetAllPrincipalListResponseModel>(_oApiResponse.data);
                getAllPrincipalListResponseModel.Succeeded = true;
            }

            return getAllPrincipalListResponseModel;

        }
        /// <summary>
        /// Returns the principal data based on the id - Developed By Harsh Chheda
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<GetPrincipalByIdResponseModel> GetPrincipalById(int Id)
        {
            GetPrincipalByIdResponseModel getPrincipalByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetPrincipalById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getPrincipalByIdResponseModel = JsonConvert.DeserializeObject<GetPrincipalByIdResponseModel>(_oApiResponse.data);
                getPrincipalByIdResponseModel.Succeeded = true;
            }

            return getPrincipalByIdResponseModel;

        }
        /// <summary>
        /// Returns the principal data based on the search parameters -Developed By Harsh Chheda
        /// </summary>
        /// <param name="SchoolName"></param>
        /// <param name="PrincipalName"></param>
        /// <param name="CreatedDate"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public async Task<GetAllPrincipalByFiltersResponseModel> GetPrincipalByFilters(string SchoolName, string PrincipalName, DateTime CreatedDate, bool IsActive)
        {
            GetAllPrincipalByFiltersResponseModel getPrincipalByFiltersResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication($"/api/v1/Principal/GetPrincipalByFilter?SchoolName={SchoolName}&PrincipalName={PrincipalName}&IsActive={IsActive}&CreatedDate={CreatedDate:yyyy/MM/dd}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getPrincipalByFiltersResponseModel = JsonConvert.DeserializeObject<GetAllPrincipalByFiltersResponseModel>(_oApiResponse.data);
                getPrincipalByFiltersResponseModel.Succeeded = true;
            }

            return getPrincipalByFiltersResponseModel;

        }
        /// <summary>
        /// Returns the Principal Data based on the page no and no of rows per page - Developed By Harsh Chheda
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<GetAllPrincipalByPaginationResponseModel> GetPrincipalByPagination(int page, int size)
        {
            GetAllPrincipalByPaginationResponseModel getAllPrincipalByPaginationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetAllPrincipalByPagination + $"?_page={page}&_size={size}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllPrincipalByPaginationResponseModel = JsonConvert.DeserializeObject<GetAllPrincipalByPaginationResponseModel>(_oApiResponse.data);
                getAllPrincipalByPaginationResponseModel.Succeeded = true;
            }

            return getAllPrincipalByPaginationResponseModel;

        }
        /// <summary>
        /// Updates the Principal Data - Developed By Harsh Chheda
        /// </summary>
        /// <param name="updatePrincipalDto"></param>
        /// <returns></returns>
        public async Task<UpdatePrincipalResponseModel> UpdatePrincipal(UpdatePrincipalDto updatePrincipalDto)
        {
            UpdatePrincipalResponseModel updatePrincipalResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updatePrincipalDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication("/api/v1/Principal/UpdatePrincipal", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updatePrincipalResponseModel = JsonConvert.DeserializeObject<UpdatePrincipalResponseModel>(_oApiResponse.data);
                if (updatePrincipalResponseModel.Succeeded)
                {
                    updatePrincipalResponseModel.Succeeded = true;
                }
                else
                {
                    updatePrincipalResponseModel.Succeeded = false;
                }
            }

            return updatePrincipalResponseModel;
        }


        public async Task<GetPrincipalByUserIdResponseModel> GetPrincipalByUserId(string UserId)
        {
            GetPrincipalByUserIdResponseModel getPrincipalByUserIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetPrincipalByUserId + $"?UserId={UserId}", HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getPrincipalByUserIdResponseModel = JsonConvert.DeserializeObject<GetPrincipalByUserIdResponseModel>(_oApiResponse.data);
                getPrincipalByUserIdResponseModel.Succeeded = true;
            }

            return getPrincipalByUserIdResponseModel;
        }


    }

}
#endregion