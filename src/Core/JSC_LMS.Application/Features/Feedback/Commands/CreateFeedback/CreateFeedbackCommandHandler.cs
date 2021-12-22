using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback
{

    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, Response<CreateFeedbackDto>>
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;


        public CreateFeedbackCommandHandler(IMapper mapper, IFeedbackRepository feedbackRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response<CreateFeedbackDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var createFeedbackCommandResponse = new Response<CreateFeedbackDto>();

            var validator = new CreateFeedbackCommandValidation();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createFeedbackCommandResponse.Succeeded = false;
                createFeedbackCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createFeedbackCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
         
            else
            {
                var data = new JSC_LMS.Domain.Entities.Feedback()
                {
                 


                  
                    SubjectId = request.createFeedbackDto.SubjectId,
                    StudentId = request.createFeedbackDto.StudentId,
                    ParentId = request.createFeedbackDto.ParentId,
                    ClassId = request.createFeedbackDto.ClassId,
                    SectionId = request.createFeedbackDto.SectionId,
                    FeedbackTitleId = request.createFeedbackDto.FeedbackTitleId,
                    FeedbackType = request.createFeedbackDto.FeedbackType,
                    FeedbackComments = request.createFeedbackDto.FeedbackComments,
                    SendDate = (DateTime)request.createFeedbackDto.SendDate,
                    //feedbackTitle = feedback.FeedbackTitle.Feedback_Title,
                    IsActive = request.createFeedbackDto.IsActive,

                    

                };
                var feedback = await _feedbackRepository.AddAsync(data);
                createFeedbackCommandResponse.Data = _mapper.Map<CreateFeedbackDto>(feedback);
                createFeedbackCommandResponse.Succeeded = true;
                createFeedbackCommandResponse.Message = "success";
            }


            return createFeedbackCommandResponse;
        }

    }
}