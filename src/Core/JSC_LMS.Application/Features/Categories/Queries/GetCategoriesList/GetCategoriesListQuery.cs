using JSC_LMS.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace JSC_LMS.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<Response<IEnumerable<CategoryListVm>>>
    {
    }
}
