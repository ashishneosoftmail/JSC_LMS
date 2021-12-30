using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    #region -developed by harsh chheda
    public interface ICityRepository
    {
        /// <summary>
        /// returns all the cities based on the state id - developed by harsh chheda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetAllCitiesResponseModel> GetAllCities(string baseurl, int id);
    }
    #endregion
}
