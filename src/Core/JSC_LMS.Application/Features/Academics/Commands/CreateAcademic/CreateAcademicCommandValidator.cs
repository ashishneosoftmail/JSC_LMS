using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Commands.CreateAcademic
{
   public class CreateAcademicCommandValidator : AbstractValidator<CreateAcademicCommand>
    {
        public CreateAcademicCommandValidator()
        {
            RuleFor(p => p._createAcademicDto.SchoolId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p._createAcademicDto.ClassId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p._createAcademicDto.SectionId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();


            RuleFor(p => p._createAcademicDto.SubjectId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p._createAcademicDto.TeacherId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p._createAcademicDto.CutOff)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p._createAcademicDto.Type)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p._createAcademicDto.IsActive)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();
        }
    }
}
