using JSC_LMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSC_LMS.Application.Features.Institutes.Queries.GetInstituteFilter
{
    public class GetInstituteFilterQuery : IRequest<Response<IEnumerable<GetInstituteFilterVm>>>
    {
        public string InstituteName { get; set; }
        public string Cityname { get; set; }
        public string Statename { get; set; }
        public bool IsActive { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public GetInstituteFilterQuery(string _InstituteName, string _City, string _State, bool _IsActive, DateTime _LicenseExpiry)
        {
            InstituteName = _InstituteName;
            Cityname = _City;
            Statename = _State;
            IsActive = _IsActive;
            LicenseExpiry = _LicenseExpiry;
        }

    }

}
