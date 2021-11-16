using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic
{
    public class UpdateAcademicCommandValidator : AbstractValidator<UpdateAcademicCommand>
    {
        public UpdateAcademicCommandValidator()
        {
            RuleFor(p => p.updateAcademicDto.SchoolId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.updateAcademicDto.ClassId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.updateAcademicDto.SectionId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();


            RuleFor(p => p.updateAcademicDto.SubjectId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.updateAcademicDto.TeacherId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.updateAcademicDto.CutOff)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.updateAcademicDto.Type)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.updateAcademicDto.IsActive)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();
        }
    }
}
