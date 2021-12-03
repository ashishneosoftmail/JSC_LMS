using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryFilter
{
   public class GetAllGallaryByFilterQuery :IRequest<Response<IEnumerable<GetAllGallaryByFilterDto>>>
    {
        public GetAllGallaryByFilterQuery(string _SchoolName, string _EventTitle )
        {
            SchoolName = _SchoolName;
            EventTitle = _EventTitle;
            
        }
        public string SchoolName { get; set; }
        public string EventTitle { get; set; }
     
 
    }

}

