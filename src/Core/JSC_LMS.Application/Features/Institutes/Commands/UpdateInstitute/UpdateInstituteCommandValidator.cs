using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute
{

    class UpdateInstituteCommandValidator : AbstractValidator<UpdateInstituteCommand>
    {
        public UpdateInstituteCommandValidator()
        {
            RuleFor(p => p.updateInstituteDto.Id)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.updateInstituteDto.AddressLine1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.updateInstituteDto.AddressLine2)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.updateInstituteDto.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.updateInstituteDto.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.updateInstituteDto.UserId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.updateInstituteDto.Username)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            /*RuleFor(p => p.updateInstituteDto.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();*/
            RuleFor(p => p.updateInstituteDto.StateId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.updateInstituteDto.ZipId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.updateInstituteDto.Mobile)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");
            RuleFor(p => p.updateInstituteDto.InstituteName)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.updateInstituteDto.ContactPerson)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.updateInstituteDto.InstituteURL)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.updateInstituteDto.LicenseExpiry)
         .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            //     RuleFor(p => p.updateInstituteDto.IsActive)
            //.NotEmpty().WithMessage("{PropertyName} is required.")
            //.NotNull();
        }

    }
}

