using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Commands.CreateSection
{
  public  class CreateSectionCommandValidation : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionCommandValidation()
        {
            RuleFor(p => p.createSectionDto.SchoolId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.createSectionDto.ClassId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.createSectionDto.SectionName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

          
        }
    }
}
