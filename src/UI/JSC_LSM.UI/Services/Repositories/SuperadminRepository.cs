using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin;
using JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminPassword;
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.Repositories
{
    public class SuperadminRepository : ISuperadminRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public SuperadminRepository()
        {

        }
        public async Task<UpdateSuperadminProfileInformationResponseModel> UpdateSuperadminPersonalInformation(UpdateSuperadminDto updateSuperadminDto)
        {
            UpdateSuperadminProfileInformationResponseModel updateSuperadminProfileInformationResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateSuperadminDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.UpdatePrincipalPersonalInformation, HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateSuperadminProfileInformationResponseModel = JsonConvert.DeserializeObject<UpdateSuperadminProfileInformationResponseModel>(_oApiResponse.data);
                if (updateSuperadminProfileInformationResponseModel.Succeeded)
                {
                    updateSuperadminProfileInformationResponseModel.Succeeded = true;
                }
                else
                {
                    updateSuperadminProfileInformationResponseModel.Succeeded = false;
                }
            }

            return updateSuperadminProfileInformationResponseModel;
        }

        public async Task<GetSuperadminByUserIdResponseModel> GetSuperadminByUserId(string id)
        {
            GetSuperadminByUserIdResponseModel getSuperadminByUserIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.GetSuperadminByUserId + id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getSuperadminByUserIdResponseModel = JsonConvert.DeserializeObject<GetSuperadminByUserIdResponseModel>(_oApiResponse.data);
                getSuperadminByUserIdResponseModel.Succeeded = true;
            }

            return getSuperadminByUserIdResponseModel;

        }

        public async Task<SuperadminChangePasswordResponseModel> SuperAdminChangePassword(UpdateSuperadminChangePasswordDto updateSuperadminChangePasswordDto)
        {
            SuperadminChangePasswordResponseModel superadminChangePasswordResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(updateSuperadminChangePasswordDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication("/api/v1/Superadmin/UpdateSuperadminChangePassword", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                superadminChangePasswordResponseModel = JsonConvert.DeserializeObject<SuperadminChangePasswordResponseModel>(_oApiResponse.data);
                superadminChangePasswordResponseModel.Succeeded = true;
            }

            return superadminChangePasswordResponseModel;
        }

        public async Task<UpdateSuperadminImageResponseModel> UpdateSuperadminImage(int Id, string LogoImageFileName, string LoginImageFileName)
        {
            UpdateSuperadminImageResponseModel updateSuperadminImageResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.UpdateSuperadminImage + $"?_Id={Id}&_LogoFileName={LogoImageFileName}&_LoginImageFileName={LoginImageFileName}", HttpMethod.Put, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                updateSuperadminImageResponseModel = JsonConvert.DeserializeObject<UpdateSuperadminImageResponseModel>(_oApiResponse.data);
                updateSuperadminImageResponseModel.Succeeded = true;
            }

            return updateSuperadminImageResponseModel;
        }
    }
}
