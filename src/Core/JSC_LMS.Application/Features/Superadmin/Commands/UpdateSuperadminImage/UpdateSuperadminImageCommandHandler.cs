using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminImage
{

    public class UpdateSuperadminImageCommandHandler : IRequestHandler<UpdateSuperAdminImageCommand, Response<int>>
    {
        private readonly ISuperadminRepository _superadminRepository;
        private readonly IMapper _mapper;
        public UpdateSuperadminImageCommandHandler(IMapper mapper, ISuperadminRepository superadminRepository)
        {
            _mapper = mapper;
            _superadminRepository = superadminRepository;
        }
        public async Task<Response<int>> Handle(UpdateSuperAdminImageCommand request, CancellationToken cancellationToken)
        {
            var superadminToUpdate = await _superadminRepository.GetByIdAsync(request.Id);
            Response<int> UpdateSuperadminCommandResponse = new Response<int>();
            if (superadminToUpdate == null)
            {
                UpdateSuperadminCommandResponse.Message = "No User Found";
                return UpdateSuperadminCommandResponse;
            }
            superadminToUpdate.LoginImage = request.LoginImageFileName;
            superadminToUpdate.Logo = request.LogoFileName;

            await _superadminRepository.UpdateAsync(superadminToUpdate);

            return new Response<int>(request.Id, " Updated successfully ");
        }
    }
}
