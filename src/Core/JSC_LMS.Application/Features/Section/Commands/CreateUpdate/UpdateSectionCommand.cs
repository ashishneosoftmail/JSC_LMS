using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Section.Commands.CreateUpdate
{
   public class UpdateSectionCommand : IRequest<Response<int>>
    {
        public UpdateSectionCommand(UpdateSectionDto _updateSectionDto)
        {
            updateSectionDto = _updateSectionDto;
        }
        public UpdateSectionDto updateSectionDto { get; set; }

    }
}
