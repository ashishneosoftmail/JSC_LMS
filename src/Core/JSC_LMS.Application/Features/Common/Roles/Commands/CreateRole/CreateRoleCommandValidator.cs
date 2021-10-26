using FluentValidation;
using JSC_LMS.Application.Contracts.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;
        public CreateRoleCommandValidator(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

            RuleFor(p => p.RoleName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.isActive)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(e => e)
                .MustAsync(RoleNameExist)
                .WithMessage("An role with the same name already exists.");

        }

        private async Task<bool> RoleNameExist(CreateRoleCommand e, CancellationToken token)
        {
            return !(await _roleRepository.IsRoleName(e.RoleName));
        }
    }
}
