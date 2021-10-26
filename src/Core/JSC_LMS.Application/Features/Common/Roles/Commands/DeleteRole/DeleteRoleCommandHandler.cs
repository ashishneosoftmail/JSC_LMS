using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Responses;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var roleToDelete = await _roleRepository.GetByIdAsync(request.Id);

            if (roleToDelete == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }
            await _roleRepository.DeleteAsync(roleToDelete);
            return Unit.Value;
        }
    }
}
