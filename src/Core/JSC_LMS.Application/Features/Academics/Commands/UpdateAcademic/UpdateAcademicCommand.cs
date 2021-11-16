using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Academics.Commands.UpdateAcademic
{
   public class UpdateAcademicCommand : IRequest<Response<int>>
    {
        public UpdateAcademicCommand(UpdateAcademicDto _updateAcademicDto)
        {
            updateAcademicDto = _updateAcademicDto;
        }
        public UpdateAcademicDto updateAcademicDto { get; set; }
    }
}
