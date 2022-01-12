using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.FeedbackTitle.Queries.GetFeedbackTitleList
{
   public class GetFeedbackTitleListQuery : IRequest<Response<IEnumerable<GetFeedbackTitleListDto>>>
    {

    }
}
