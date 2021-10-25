using JSC_LMS.Application.Responses;
using JSC_LMS.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace JSC_LMS.Application.Features.Common.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<Response<IEnumerable<Category>>>
    {

    }
}
