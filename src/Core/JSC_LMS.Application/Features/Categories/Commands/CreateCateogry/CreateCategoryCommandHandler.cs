using AutoMapper;
using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Application.Responses;
using JSC_LMS.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Features.Categories.Commands.CreateCateogry
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CreateCategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<CreateCategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var createCategoryCommandResponse = new Response<CreateCategoryDto>();

            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createCategoryCommandResponse.Succeeded = false;
                createCategoryCommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createCategoryCommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var category = new Category() { CategoryName = request.Name };
                category = await _categoryRepository.AddAsync(category);
                createCategoryCommandResponse.Data = _mapper.Map<CreateCategoryDto>(category);
                createCategoryCommandResponse.Succeeded = true;
                createCategoryCommandResponse.Message = "success";
            }

            return createCategoryCommandResponse;
        }
    }
}
