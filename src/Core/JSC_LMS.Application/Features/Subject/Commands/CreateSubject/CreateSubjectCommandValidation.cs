using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Subject.Commands.CreateSubject
{
  public  class CreateSubjectCommandValidation : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidation()
        {
            RuleFor(p => p.createSubjectDto.SchoolId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.createSubjectDto.ClassId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.createSubjectDto.SectionId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.createSubjectDto.SubjectName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

        
        }
    }
}
