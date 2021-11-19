using JSC_LMS.Application.Features.ParentsFeature.Commands.CreateParents;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IParentsRepository
    {
        Task<ParentsResponseModel> AddNewParents(CreateParentsDto createParentsDto);
    }
}
