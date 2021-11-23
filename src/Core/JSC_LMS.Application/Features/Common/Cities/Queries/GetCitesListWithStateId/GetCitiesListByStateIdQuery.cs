using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace JSC_LMS.Application.Features.Common.Cities.Queries.GetCitiesListWithStateId
{
    public class GetCitiesListByStateIdQuery : IRequest<Response<IEnumerable<City>>>
    {
        public int StateId { get; set; }
    }
}
