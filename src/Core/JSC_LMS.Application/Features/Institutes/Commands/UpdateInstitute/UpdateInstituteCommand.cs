using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

#region -Command for update institute :by Shivani Goswami
namespace JSC_LMS.Application.Features.Institutes.Commands.UpdateInstitute
{
    public class UpdateInstituteCommand : IRequest<Response<int>>
    {
        public UpdateInstituteCommand(UpdateInstituteDto _updateInstituteDto)
        {
            updateInstituteDto = _updateInstituteDto;
        }
        public UpdateInstituteDto updateInstituteDto { get; set; }
    }
}
#endregion