using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstituteAdminProfileInformation
{

    public class UpdateInstituteAdminProfileInformationCommandHandler : IRequestHandler<UpdateInstituteAdminProfileInformationCommand, Response<int>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UpdateInstituteAdminProfileInformationCommandHandler(IMapper mapper, IInstituteRepository instituteRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _instituteRepository = instituteRepository;
            _authenticationService = authenticationService;
        }
        public async Task<Response<int>> Handle(UpdateInstituteAdminProfileInformationCommand request, CancellationToken cancellationToken)
        {
            var instituteadminToUpdate = await _instituteRepository.GetByIdAsync(request.updateInstituteAdminProfileInformationDto.Id);
            Response<int> UpdateSuperadminCommandResponse = new Response<int>();
            if (instituteadminToUpdate == null)
            {
                UpdateSuperadminCommandResponse.Message = "No User Found";
                return UpdateSuperadminCommandResponse;
            }
            instituteadminToUpdate.ContactPerson = request.updateInstituteAdminProfileInformationDto.Name;
            instituteadminToUpdate.Mobile = request.updateInstituteAdminProfileInformationDto.Mobile;

            await _instituteRepository.UpdateAsync(instituteadminToUpdate);

            return new Response<int>(request.updateInstituteAdminProfileInformationDto.Id, " Updated successfully ");
        }
    }
}
