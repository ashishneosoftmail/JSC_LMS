using AutoMapper;
using JSC_LMS.Application.Contracts.Identity;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackById
{
    public class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, Response<GetFeedbackByIdDto>>
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IParentsRepository _parentsRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IFeedbackTitleRepository _feedbackTitleRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public GetFeedbackByIdQueryHandler(IMapper mapper, IFeedbackRepository feedbackRepository, IClassRepository classRepository, IStudentRepository studentRepository, IParentsRepository parentsRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, IFeedbackTitleRepository feedbackTitleRepository,IAuthenticationService authenticationService, ILogger<GetFeedbackByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _parentsRepository = parentsRepository;
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _feedbackTitleRepository = feedbackTitleRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }



        public async Task<Response<GetFeedbackByIdDto>> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            Response<GetFeedbackByIdDto> responseData = new Response<GetFeedbackByIdDto>();
            var feedback = await _feedbackRepository.GetByIdAsync(request.Id);
            if (feedback == null)
            {
                responseData.Succeeded = true;
                responseData.Message = "Data Doesn't Exist";
                responseData.Data = null;
                return responseData;
            }



            var feedbackData = new GetFeedbackByIdDto()
            {
                Id = feedback.Id,
                FeedbackTitleId = feedback.FeedbackTitleId,
                SubjectId = feedback.SubjectId,
                StudentId = feedback.StudentId,
                ParentId = feedback.ParentId,
                Type = feedback.FeedbackType,
                FeedbackComments = feedback.FeedbackComments,
                SendDate = (DateTime)feedback.SendDate,
                //feedbackTitle = feedback.FeedbackTitle.Feedback_Title,
                IsActive = feedback.IsActive,
               

                Classes = new ClassDto()
                {
                    classId = feedback.ClassId,
                    ClassName = (await _classRepository.GetByIdAsync(feedback.ClassId)).ClassName
                },
                Students = new StudentDto()
                {
                    Id = feedback.StudentId,
                    StudentName = (await _studentRepository.GetByIdAsync(feedback.StudentId)).StudentName
                },
                Parents = new ParentDto()
                {
                    parentId = feedback.ParentId,
                    ParentName = (await _parentsRepository.GetByIdAsync(feedback.ParentId)).ParentName
                },

                Section = new SectionDto()
                {
                    sectionId = feedback.SectionId,
                    SectionName = (await _sectionRepository.GetByIdAsync(feedback.SectionId)).SectionName
                },

                Subject = new SubjectDto()
                {
                    subjectId = feedback.SubjectId,
                    SubjectName = (await _subjectRepository.GetByIdAsync(feedback.SubjectId)).SubjectName
                },

                feedbackTitle = new FeedbackTitleDto()
                {
                    feedbacktitleId = feedback.FeedbackTitleId,
                    Feedback_Title = (await _feedbackTitleRepository.GetByIdAsync(feedback.FeedbackTitleId)).Feedback_Title
                },

            };
            _logger.LogInformation("Hanlde Completed");
            return new Response<GetFeedbackByIdDto>(feedbackData, "success");
        }

    }
}
