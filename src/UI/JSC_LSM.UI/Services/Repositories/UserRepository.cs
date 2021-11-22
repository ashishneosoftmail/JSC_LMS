using JSC_LMS.Application.Features.ManageProfile.ChangePassword;
using JSC_LMS.Application.Features.ManageProfile.UpdateProfileInfo;
using JSC_LMS.Application.Models.Authentication;
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
    public class UserRepository : IUserRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;

        public UserRepository()
        {

        }

        public async Task<AuthenticationResponseModel> UserAuthenticate(AuthenticationRequest authenticateRequest)
        {
            AuthenticationResponseModel authenticationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(authenticateRequest, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.UserAuthenticate, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                authenticationResponseModel = JsonConvert.DeserializeObject<AuthenticationResponseModel>(_oApiResponse.data);
                if (authenticationResponseModel.isAuthenticated)
                {
                    authenticationResponseModel.isSuccess = true;
                }
                else
                {
                    authenticationResponseModel.isSuccess = false;
                }
            }

            return authenticationResponseModel;

        }

        public async Task<ChangePasswordResponseModel> UpdateChangePassword(ChangePasswordDto changePasswordDto)
        {
            ChangePasswordResponseModel changePasswordResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);
            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(changePasswordDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.UpdateChangePassword, HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                changePasswordResponseModel = JsonConvert.DeserializeObject<ChangePasswordResponseModel>(_oApiResponse.data);
                changePasswordResponseModel.Succeeded = true;
            }
            return changePasswordResponseModel;
        }


        public async Task<UpdateProfileInformationResponseModel> UpdatePersonalInformation(UpdateProfileInfoDto updateProfileInformationDto)
        {
            UpdateProfileInformationResponseModel updateProfileInformationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateProfileInformationDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.UpdateProfileInformation, HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateProfileInformationResponseModel = JsonConvert.DeserializeObject<UpdateProfileInformationResponseModel>(_oApiResponse.data);
                if (updateProfileInformationResponseModel.Succeeded)
                {
                    updateProfileInformationResponseModel.Succeeded = true;
                }
                else
                {
                    updateProfileInformationResponseModel.Succeeded = false;
                }
            }
            return updateProfileInformationResponseModel;
        }


    }
}
