using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Categories.Queries.GetCategoriesList
{
    public class GetStatesListQueryHandler : IRequestHandler<GetCategoriesListQuery, Response<IEnumerable<Category>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetStatesListQueryHandler(IMapper mapper, ICategoryRepository categoryRepository, ILogger<GetStatesListQueryHandler> logger)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<Category>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");
            var allCategories = await _categoryRepository.ListAllAsync();
            var categories = _mapper.Map<IEnumerable<Category>>(allCategories);
            _logger.LogInformation("Hanlde Completed");
            return new Response<IEnumerable<Category>>(categories, "success");

        }
    }
}
