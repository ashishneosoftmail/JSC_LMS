using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherCommandValidator()
        {
            RuleFor(p => p.updateTeacherDto.AddressLine1)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.updateTeacherDto.AddressLine2)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.updateTeacherDto.CityId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.updateTeacherDto.StateId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.updateTeacherDto.TeacherName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.updateTeacherDto.UserType)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.updateTeacherDto.ZipId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.updateTeacherDto.SectionId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();

            RuleFor(p => p.updateTeacherDto.ClassId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.updateTeacherDto.SubjectId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
            RuleFor(p => p.updateTeacherDto.Mobile)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");

         
           
        }


            }


    }

