using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Principal.Commands.CreatePrincipal
{

    public class CreatePrincipalCommandValidation : AbstractValidator<CreatePrincipalCommand>
    {
        public CreatePrincipalCommandValidation()
        {
            RuleFor(p => p.createPrincipalDto.SchoolId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createPrincipalDto.AddressLine1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createPrincipalDto.Name)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createPrincipalDto.AddressLine2)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createPrincipalDto.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
           
         
            RuleFor(p => p.createPrincipalDto.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createPrincipalDto.StateId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.createPrincipalDto.ZipId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createPrincipalDto.Mobile)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");
            /*RuleFor(p => p.createPrincipalDto.IsActive)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();*/
            RuleFor(p => p.createPrincipalDto.RoleName)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();
        }

    }
}
