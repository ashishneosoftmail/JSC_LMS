using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.UpdateSubject
{
   public class UpdateSubjectCommandValidation : AbstractValidator<UpdateSubjectCommand>
    {
        public UpdateSubjectCommandValidation()
        {
            RuleFor(p => p.updateSubjectDto.SchoolId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.updateSubjectDto.ClassId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.updateSubjectDto.SectionId)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();

            RuleFor(p => p.updateSubjectDto.SubjectName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.updateSubjectDto.IsActive)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();
        }
    }
}
