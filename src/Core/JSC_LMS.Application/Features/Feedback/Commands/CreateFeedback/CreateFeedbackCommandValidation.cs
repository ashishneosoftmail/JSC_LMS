using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback
{
    public class CreateFeedbackCommandValidation : AbstractValidator<CreateFeedbackCommand>
    {
        public CreateFeedbackCommandValidation()
        {
            //RuleFor(p => p.createFeedbackDto.Id)
            //   .NotEmpty().WithMessage("{PropertyName} is required.")
            //   .NotNull();
            RuleFor(p => p.createFeedbackDto.FeedbackTitleId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();
  
            RuleFor(p => p.createFeedbackDto.StudentId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();
            RuleFor(p => p.createFeedbackDto.ParentId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.createFeedbackDto.SubjectId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
            RuleFor(p => p.createFeedbackDto.SendDate)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();


        }

    }
}
