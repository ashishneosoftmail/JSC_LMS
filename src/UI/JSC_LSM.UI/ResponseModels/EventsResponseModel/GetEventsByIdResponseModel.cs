using JSC_LMS.Application.Features.EventsFeature.Queries.GetEventsById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.EventsResponseModel
{
    public class GetEventsByIdResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public GetEventsByIdDto data { get; set; }
    }
}
