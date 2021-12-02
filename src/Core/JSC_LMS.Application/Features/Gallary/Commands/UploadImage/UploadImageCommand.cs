using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Gallary.Commands.UploadImage
{
   public class UploadImageCommand : IRequest<Response<UploadImageDto>>
    {
        public UploadImageCommand(UploadImageDto _uploadImageDto)
        {
            uploadImageDto = _uploadImageDto;
        }
        public UploadImageDto uploadImageDto { get; set; }
    }
}
