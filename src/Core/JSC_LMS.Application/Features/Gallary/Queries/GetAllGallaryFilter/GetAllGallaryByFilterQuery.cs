using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Queries.GetAllGallaryFilter
{
   public class GetAllGallaryByFilterQuery :IRequest<Response<IEnumerable<GetAllGallaryByFilterDto>>>
    {
        public GetAllGallaryByFilterQuery(int _SchoolId)
        {
            SchoolId = _SchoolId;
        }
        public int SchoolId{ get; set; }
       
    }

}

