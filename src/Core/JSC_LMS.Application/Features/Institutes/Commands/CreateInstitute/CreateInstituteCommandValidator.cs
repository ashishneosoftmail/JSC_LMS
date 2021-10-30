using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute
{
    public class CreateInstituteCommandValidator : AbstractValidator<CreateInstituteCommand>
    {
        public CreateInstituteCommandValidator()
        {
            RuleFor(p => p.createInstituteDto.AddressLine1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createInstituteDto.AddressLine2)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createInstituteDto.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createInstituteDto.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createInstituteDto.Username)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createInstituteDto.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createInstituteDto.StateId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.createInstituteDto.ZipId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createInstituteDto.Mobile)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");
            RuleFor(p => p.createInstituteDto.InstituteName)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.createInstituteDto.ContactPerson)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.createInstituteDto.InstituteURL)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.createInstituteDto.LicenseExpiry)
         .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
       //     RuleFor(p => p.createInstituteDto.IsActive)
       //.NotEmpty().WithMessage("{PropertyName} is required.")
       //.NotNull();
        }

    }
}
