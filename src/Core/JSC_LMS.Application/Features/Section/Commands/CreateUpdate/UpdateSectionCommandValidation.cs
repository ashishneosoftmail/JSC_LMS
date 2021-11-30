using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Commands.CreateUpdate
{
    class UpdateSectionCommandValidation : AbstractValidator<UpdateSectionCommand>
    {
        public UpdateSectionCommandValidation()
        {
            RuleFor(p => p.updateSectionDto.SchoolId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.updateSectionDto.ClassId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.updateSectionDto.SectionName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

        
        }
    }
}
