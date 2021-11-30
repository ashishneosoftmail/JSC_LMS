using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Class.Commands.CreateClass
{
   public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            RuleFor(p => p.createClassDto.SchoolId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.createClassDto.ClassName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
           
        }
    }
}
