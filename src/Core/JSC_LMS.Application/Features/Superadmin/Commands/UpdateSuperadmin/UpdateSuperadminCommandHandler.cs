using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadmin
{
    public class UpdateSuperadminCommandHandler : IRequestHandler<UpdateSuperadminCommand, Response<int>>
    {
        private readonly ISuperadminRepository _superadminRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public UpdateSuperadminCommandHandler(IMapper mapper, ISuperadminRepository superadminRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _superadminRepository = superadminRepository;
            _authenticationService = authenticationService;
        }
        public async Task<Response<int>> Handle(UpdateSuperadminCommand request, CancellationToken cancellationToken)
        {
            var superadminToUpdate = await _superadminRepository.GetByIdAsync(request.updateSuperadminDto.Id);
            Response<int> UpdateSuperadminCommandResponse = new Response<int>();
            if (superadminToUpdate == null)
            {
                UpdateSuperadminCommandResponse.Message = "No User Found";
                return UpdateSuperadminCommandResponse;
            }
            superadminToUpdate.Name = request.updateSuperadminDto.Name;
            superadminToUpdate.EmailSupport = request.updateSuperadminDto.EmailSupport;
            superadminToUpdate.MobileSupport = request.updateSuperadminDto.MobileSupport;

            await _superadminRepository.UpdateAsync(superadminToUpdate);

            return new Response<int>(request.updateSuperadminDto.Id, " Updated successfully ");
        }
    }
}
