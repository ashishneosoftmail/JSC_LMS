using JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchoolClassSectionPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllAnnouncementListBySchoolClassSectionPaginationResponseModel
    {
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAllAnnouncementListBySchoolClassSectionPaginationDto> data { get; set; }
    }
}
