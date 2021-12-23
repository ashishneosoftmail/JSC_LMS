using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Feedback.Queries.GetFeedbackList
{
    public class GetAllFeedbackListQuery : IRequest<Response<IEnumerable<GetAllFeedbackListVm>>>
    {
    }
}
