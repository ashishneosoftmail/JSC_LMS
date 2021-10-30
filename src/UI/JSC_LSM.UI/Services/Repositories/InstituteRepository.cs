using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.Repositories
{
    public class InstituteRepository
    {
    }

    //public class InstituteRepository : IUserRepository
    //{
    //    private APIRepository _aPIRepository;
    //    private APICommunicationResponseModel<string> _oApiResponse;
    //    private string _sToken = string.Empty;
    //    private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
    //    private readonly IConfiguration _configuration;

    //    public UserRepository()
    //    {

    //    }

    //    public async Task<AuthenticationResponseModel> UserAuthenticate(AuthenticationRequest authenticateRequest)
    //    {
    //        AuthenticationResponseModel authenticationResponseModel = null;
    //        _aPIRepository = new APIRepository(_configuration);

    //        _oApiResponse = new APICommunicationResponseModel<string>();
    //        var json = JsonConvert.SerializeObject(authenticateRequest, Formatting.Indented);
    //        byte[] content = Encoding.ASCII.GetBytes(json);
    //        var bytes = new ByteArrayContent(content);

    //        _oApiResponse = await _aPIRepository.APICommunication(UrlHelper.UserAuthenticate, HttpMethod.Post, bytes, _sToken);
    //        if (_oApiResponse.data != null)
    //        {
    //            authenticationResponseModel = JsonConvert.DeserializeObject<AuthenticationResponseModel>(_oApiResponse.data);
    //            if (authenticationResponseModel.isAuthenticated)
    //            {
    //                authenticationResponseModel.isSuccess = true;
    //            }
    //            else
    //            {
    //                authenticationResponseModel.isSuccess = false;
    //            }
    //        }

    //        return authenticationResponseModel;

    //    }
    //}
}
