﻿using JSC_LMS.Application.Models.Authentication;
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

        public async Task<TemporaryPasswordEmailValidateResponse> TemporaryPasswordEmailValidate(string email)
        {
            TemporaryPasswordEmailValidateResponse temporaryPasswordEmailValidateResponse = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.ForgotPasswordValidateEmail + $"?email={email}", HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.Success)
            {
                temporaryPasswordEmailValidateResponse = JsonConvert.DeserializeObject<TemporaryPasswordEmailValidateResponse>(_oApiResponse.data);
                if (temporaryPasswordEmailValidateResponse.Succeeded)
                {
                    temporaryPasswordEmailValidateResponse.isSuccess = true;
                }
                else
                {
                    temporaryPasswordEmailValidateResponse.isSuccess = false;
                }
                return temporaryPasswordEmailValidateResponse;
            }
            else
            {
                temporaryPasswordEmailValidateResponse = JsonConvert.DeserializeObject<TemporaryPasswordEmailValidateResponse>(_oApiResponse.data);
                if (temporaryPasswordEmailValidateResponse.Succeeded)
                {
                    temporaryPasswordEmailValidateResponse.isSuccess = true;
                }
                else
                {
                    temporaryPasswordEmailValidateResponse.isSuccess = false;
                }
                return temporaryPasswordEmailValidateResponse;
            }


        }
        public async Task<VerifyTemporaryPasswordResponse> VerfiyTemporaryPassword(VerfiyTemporaryPasswordRequest verfiyTemporaryPasswordRequest)
        {
            VerifyTemporaryPasswordResponse verifyTemporaryPasswordResponse = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(verfiyTemporaryPasswordRequest, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);


            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.VerifyTemporaryPassword, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.Success)
            {
                verifyTemporaryPasswordResponse = JsonConvert.DeserializeObject<VerifyTemporaryPasswordResponse>(_oApiResponse.data);
                if (verifyTemporaryPasswordResponse.Succeeded)
                {
                    verifyTemporaryPasswordResponse.isSuccess = true;
                }
                else
                {
                    verifyTemporaryPasswordResponse.isSuccess = false;
                }
                return verifyTemporaryPasswordResponse;
            }
            else
            {
                verifyTemporaryPasswordResponse = JsonConvert.DeserializeObject<VerifyTemporaryPasswordResponse>(_oApiResponse.data);
                if (verifyTemporaryPasswordResponse.Succeeded)
                {
                    verifyTemporaryPasswordResponse.isSuccess = true;
                }
                else
                {
                    verifyTemporaryPasswordResponse.isSuccess = false;
                }
                return verifyTemporaryPasswordResponse;
            }

        }
    }
}
