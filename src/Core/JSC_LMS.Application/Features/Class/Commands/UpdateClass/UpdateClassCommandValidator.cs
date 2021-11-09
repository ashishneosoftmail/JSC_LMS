using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.UpdateClass
{
  public  class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            RuleFor(p => p.updateClassDto.SchoolId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.updateClassDto.ClassName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

           /* RuleFor(p => p.updateClassDto.IsActive)
       .NotEmpty().WithMessage("{PropertyName} is required.")
       .NotNull();*/
        }

    }
}
