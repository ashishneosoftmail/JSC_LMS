using JSC_LMS.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Commands.UpdateSuperadminImage
{
    public class UpdateSuperAdminImageCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string LogoFileName { get; set; }
        public string LoginImageFileName { get; set; }
    }
}
