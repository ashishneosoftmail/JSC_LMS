using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Common
{
    public class Common
    {
        private readonly IStateRepository _stateRepository;
        public Common(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        [NonAction]
        public async Task<List<SelectListItem>> GetAllStates()
        {
            List<SelectListItem> state = new List<SelectListItem>();
            GetAllStatesResponseModel getAllStateResponseModel = null;
            ResponseModel responseModel = new ResponseModel();
            getAllStateResponseModel = await _stateRepository.GetAllStates();

            if (getAllStateResponseModel.isSuccess)
            {
                if (getAllStateResponseModel == null && getAllStateResponseModel.data == null)
                {
                    responseModel.ResponseMessage = getAllStateResponseModel.message;
                    responseModel.IsSuccess = getAllStateResponseModel.isSuccess;
                }
                if (getAllStateResponseModel != null)
                {
                    //User user = authenticationResponseModel.userDetail;
                    responseModel.ResponseMessage = getAllStateResponseModel.message;
                    responseModel.IsSuccess = getAllStateResponseModel.isSuccess;
                    foreach (var item in getAllStateResponseModel.data)
                    {
                        state.Add(new SelectListItem
                        {
                            Text = item.StateName,
                            Value = Convert.ToString(item.Id)
                        });
                    }
                    return state;
                }
            }
            else
            {
                responseModel.ResponseMessage = getAllStateResponseModel.message;
                responseModel.IsSuccess = getAllStateResponseModel.isSuccess;
            }
            return null;
        }
    }
}
