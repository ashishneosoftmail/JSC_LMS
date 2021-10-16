using JSC_LMS.Application.Responses;
using MediatR;

namespace JSC_LMS.Application.Features.Categories.Commands.CreateCateogry
{
    public class CreateCategoryCommand : IRequest<Response<CreateCategoryDto>>
    {
        public string Name { get; set; }
    }
}
