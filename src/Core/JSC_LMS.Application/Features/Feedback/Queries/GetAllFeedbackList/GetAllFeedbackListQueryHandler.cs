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

namespace JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList
{
    public class GetAllFeedbackListQueryHandler : IRequestHandler<GetAllFeedbackListQuery, Response<IEnumerable<GetAllFeedbackListVm>>>
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
        public GetAllFeedbackListQueryHandler(IMapper mapper, IFeedbackRepository feedbackRepository, IClassRepository classRepository, IStudentRepository studentRepository, IParentsRepository parentsRepository, ISectionRepository sectionRepository, ISubjectRepository subjectRepository, IFeedbackTitleRepository feedbackTitleRepository, IAuthenticationService authenticationService, ILogger<GetAllFeedbackListQueryHandler> logger)
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
        public async Task<Response<IEnumerable<GetAllFeedbackListVm>>> Handle(GetAllFeedbackListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allFeedback = await _feedbackRepository.ListAllAsync();
            List<GetAllFeedbackListVm> feedbackList = new List<GetAllFeedbackListVm>();
            foreach (var feedback in allFeedback)
            {

                feedbackList.Add(new GetAllFeedbackListVm()
                {
                    Id = feedback.Id,
                    SubjectId = feedback.SubjectId,
                    StudentId = feedback.StudentId,
                    ParentId = feedback.ParentId,
                    ClassId = feedback.ClassId,
                    SectionId = feedback.SectionId,
                    FeedbackComments = feedback.FeedbackComments,
                    FeedbackType = feedback.FeedbackType,
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


                });
            }
            /* var principal = _mapper.Map<List<GetPrincipalListDto>>(allPrincipal);
             _logger.LogInformation("Hanlde Completed");
             return new Response<IEnumerable<GetPrincipalListDto>>(principal, "success");*/
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<GetAllFeedbackListVm>>(feedbackList, "success");
        }

    }
}
