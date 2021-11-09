using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Commands.UpdatePrincipal
{

    public class UpdatePrincipalCommandValidator : AbstractValidator<UpdatePrincipalCommand>
    {
        public UpdatePrincipalCommandValidator()
        {
            RuleFor(p => p.updatePrincipalDto.Id)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.updatePrincipalDto.SchoolId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();
            RuleFor(p => p.updatePrincipalDto.AddressLine1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.updatePrincipalDto.AddressLine2)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.updatePrincipalDto.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.updatePrincipalDto.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.updatePrincipalDto.UserId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.updatePrincipalDto.Username)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            /*RuleFor(p => p.updatePrincipalDto.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();*/
            RuleFor(p => p.updatePrincipalDto.StateId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.updatePrincipalDto.ZipId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.updatePrincipalDto.Mobile)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");
            RuleFor(p => p.updatePrincipalDto.Name)
         .NotEmpty().WithMessage("{PropertyName} is required.")
         .NotNull().MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            //     RuleFor(p => p.updatePrincipalDto.IsActive)
            //.NotEmpty().WithMessage("{PropertyName} is required.")
            //.NotNull();
        }

    }
}
