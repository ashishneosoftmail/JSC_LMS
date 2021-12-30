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
    #region - developed by harsh chheda
    public class RolesRepository : IRoleRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly IConfiguration _configuration;
        public RolesRepository()
        {

        }
        /// <summary>
        /// returns all the role - developed by harsh chheda
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllRolesResponseModel> GetAllRoles(string baseurl)
        {
            GetAllRolesResponseModel getAllRolesResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication(baseurl,UrlHelper.GetAllRole, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllRolesResponseModel = JsonConvert.DeserializeObject<GetAllRolesResponseModel>(_oApiResponse.data);
                getAllRolesResponseModel.isSuccess = true;
            }

            return getAllRolesResponseModel;

        }
    }
    #endregion
}
