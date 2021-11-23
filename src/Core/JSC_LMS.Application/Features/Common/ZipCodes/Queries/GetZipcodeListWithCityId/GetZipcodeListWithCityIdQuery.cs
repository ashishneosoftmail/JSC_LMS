using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Common.ZipCodes.Queries.GetZipcodeListWithCityId
{
    public class GetZipcodeListWithCityIdQuery : IRequest<Response<IEnumerable<Zip>>>
    {
        public int CityId { get; set; }
    }
}
