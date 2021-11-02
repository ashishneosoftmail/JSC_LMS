﻿using JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute;
using JSC_LSM.UI.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Services.IRepositories
{
    public interface IInstituteRepository
    {
        Task<InstituteResponseModel> CreateInstitute(CreateInstituteDto createInstituteDto);
        Task<GetAllInstituteListResponseModel> GetAllInstituteDetails();
    }
}
