using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Commands.CreateSection
{
    public class CreateSectionCommand : IRequest<Response<CreateSectionDto>>
    {
        public CreateSectionDto createSectionDto { get; set; }

    }
}
