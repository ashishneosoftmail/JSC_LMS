using JSC_LMS.Application.Features.Announcement.Queries.GetAnnouncementListByPaginationBySchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllAnnouncementListBySchoolPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAnnouncementListByPaginationBySchoolDto> data { get; set; }
    }
}
