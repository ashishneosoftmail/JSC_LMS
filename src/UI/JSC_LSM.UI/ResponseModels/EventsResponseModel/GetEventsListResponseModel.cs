using JSC_LMS.Application.Features.EventsFeature.Queries.GetAllEventsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels.EventsResponseModel
{
    public class GetEventsListResponseModel
    {
        public bool isSuccess { get; set; }
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAllEventsListDto> data { get; set; }
    }
}
