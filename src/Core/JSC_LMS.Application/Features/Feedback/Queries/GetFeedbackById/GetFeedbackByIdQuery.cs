using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackById
{
    public class GetFeedbackByIdQuery : IRequest<Response<GetFeedbackByIdDto>>
    {
        public int Id { get; set; }
    }
}
