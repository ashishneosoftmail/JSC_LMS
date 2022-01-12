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

namespace JSC_LMS.Application.Features.FeedbackTitle.Queries.GetFeedbackTitleList
{
    public class GetFeedbackTitleListQueryHandler : IRequestHandler<GetFeedbackTitleListQuery, Response<IEnumerable<GetFeedbackTitleListDto>>>
    {
        private readonly IFeedbackTitleRepository _feedbackTitleRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetFeedbackTitleListQueryHandler(IMapper mapper, IFeedbackTitleRepository feedbackTitleRepository, IAuthenticationService authenticationService, ILogger<GetFeedbackTitleListQueryHandler> logger)
        {
            _mapper = mapper;
            _feedbackTitleRepository = feedbackTitleRepository;
            _logger = logger;
            _authenticationService = authenticationService;

        }
        public async Task<Response<IEnumerable<GetFeedbackTitleListDto>>> Handle(GetFeedbackTitleListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allFeedback = await _feedbackTitleRepository.ListAllAsync();
            List<GetFeedbackTitleListDto> feedbackList = new List<GetFeedbackTitleListDto>();
            foreach (var feedback in allFeedback)
            {

                feedbackList.Add(new GetFeedbackTitleListDto()
                {
                    Id = feedback.Id,
                    FeedbackTitle = feedback.Feedback_Title



                });
            }
                _logger.LogInformation("Hanlde Completed");
                return new Response<IEnumerable<GetFeedbackTitleListDto>>(feedbackList, "success");
            }
        }
    }



    
