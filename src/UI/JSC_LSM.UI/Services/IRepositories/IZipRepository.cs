using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    #region - developed by harsh chheda
    public interface IZipRepository
    {
        /// <summary>
        /// returns all the zip code based on the cityid -developed by harsh chheda
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        Task<GetAllZipResponseModel> GetAllZipcodes(int cityId);
    }
    #endregion
}
