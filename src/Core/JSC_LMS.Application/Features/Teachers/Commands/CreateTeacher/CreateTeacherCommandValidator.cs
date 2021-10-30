using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Commands.CreateTeacher
{
     public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(p => p.createTeacherDto.AddressLine1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createTeacherDto.AddressLine2)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createTeacherDto.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.createTeacherDto.StateId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.createTeacherDto.TeacherName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.createTeacherDto.UserTypeId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            
            RuleFor(p => p.createTeacherDto.ZipId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createTeacherDto.SchoolId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createTeacherDto.SectionId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            
            RuleFor(p => p.createTeacherDto.ClassId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createTeacherDto.SubjectId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.createTeacherDto.Mobile)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");
            
            RuleFor(p => p.createTeacherDto.Email)
          .NotEmpty().WithMessage("{PropertyName} is required.")
          .NotNull().MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.createTeacherDto.IsActive)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();
        }
    }
}
