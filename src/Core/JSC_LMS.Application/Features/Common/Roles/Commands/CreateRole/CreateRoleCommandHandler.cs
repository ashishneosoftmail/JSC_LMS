using AutoMapper;
using JSC_LMS.Application.Contracts.Infrastructure;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Models.Mail;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<int>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoleCommandHandler> _logger;


        public CreateRoleCommandHandler(IMapper mapper, IRoleRepository roleRepository, ILogger<CreateRoleCommandHandler> logger)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<Response<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var validator = new CreateRoleCommandValidator(_roleRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @role = _mapper.Map<Role>(request);

            @role = await _roleRepository.AddAsync(@role);

            var response = new Response<int>(@role.Id, "Inserted successfully ");

            _logger.LogInformation("Handle Completed");

            return response;
        }
    }
}