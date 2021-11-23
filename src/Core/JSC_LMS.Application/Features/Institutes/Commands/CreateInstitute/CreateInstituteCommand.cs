using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
#region -Command for create institute :by Shivani Goswami
namespace JSC_LMS.Application.Features.Institutes.Commands.CreateInstitute
{
    public class CreateInstituteCommand : IRequest<Response<CreateInstituteDto>>
    {
        public CreateInstituteDto createInstituteDto { get; set; }
        public CreateInstituteCommand(CreateInstituteDto _createInstituteDto)
        {
            createInstituteDto = _createInstituteDto;
        }
    }
}
#endregion