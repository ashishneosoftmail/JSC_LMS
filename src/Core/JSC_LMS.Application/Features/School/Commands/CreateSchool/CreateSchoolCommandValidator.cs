using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.School.Commands.CreateSchool
{
    public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
    {
        public CreateSchoolCommandValidator()
        {
            RuleFor(p => p.createSchoolDto.Address1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createSchoolDto.Address2)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createSchoolDto.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createSchoolDto.StateId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.createSchoolDto.ZipId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createSchoolDto.InstituteId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createSchoolDto.Mobile)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");
            RuleFor(p => p.createSchoolDto.SchoolName)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.createSchoolDto.ContactPerson)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
            RuleFor(p => p.createSchoolDto.Email)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createSchoolDto.IsActive)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();
        }
    }
}
