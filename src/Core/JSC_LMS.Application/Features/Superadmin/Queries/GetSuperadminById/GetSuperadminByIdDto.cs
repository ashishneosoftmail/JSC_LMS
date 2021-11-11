using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Superadmin.Queries.GetSuperadminById
{
    public class GetSuperadminByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileSupport { get; set; }
        public string EmailSupport { get; set; }
        public byte[] Logo { get; set; }
        public byte[] LoginImage { get; set; }
    }
}
