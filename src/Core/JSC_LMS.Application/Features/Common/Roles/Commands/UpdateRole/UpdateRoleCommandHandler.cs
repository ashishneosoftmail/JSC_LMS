using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Exceptions;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response<int>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<Response<int>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {

            var roleToUpdate = await _roleRepository.GetByIdAsync(request.Id);

            if (roleToUpdate == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            var validator = new UpdateRoleCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, roleToUpdate, typeof(UpdateRoleCommand), typeof(Role));

            await _roleRepository.UpdateAsync(roleToUpdate);

            return new Response<int>(request.Id, "Updated successfully ");

        }
    }
}