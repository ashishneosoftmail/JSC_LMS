using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Response;
using JSC_LMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Common.Categories.Commands
{

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;


        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, ILogger<CreateCategoryCommandHandler> logger)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Response<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle Initiated");

            var @category = new Category() { CategoryName = request.CategoryName, IsActive = request.IsActive };

            var _category = await _categoryRepository.AddAsync(@category);

            var response = new Response<int>(_category.Id, "Inserted successfully ");

            _logger.LogInformation("Handle Completed");

            return response;
        }
    }
}
