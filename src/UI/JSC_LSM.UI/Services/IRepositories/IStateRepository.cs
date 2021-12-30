using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    #region- developed by harsh chheda
    public interface IStateRepository
    {
        /// <summary>
        /// Returns all the state -developed by harsh chheda
        /// </summary>
        /// <returns></returns>
        Task<GetAllStatesResponseModel> GetAllStates(string baseurl);
    }
    #endregion
}
