using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesListWithEventsQueryHandler : IRequestHandler<GetCategoriesListWithEventsQuery, Response<IEnumerable<CategoryEventListVm>>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesListWithEventsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<IEnumerable<CategoryEventListVm>>> Handle(GetCategoriesListWithEventsQuery request, CancellationToken cancellationToken)
        {
            /* var list = await _categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);
             var categoryEventList = _mapper.Map<IEnumerable<CategoryEventListVm>>(list);
             return new Response<IEnumerable<CategoryEventListVm>>(categoryEventList, "success");*/
            return null;
        }


    }
}
