using JSC_LMS.Application.Features.Announcement.Queries.GetAllAnnouncementListBySchool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.ResponseModels
{
    public class GetAllAnnouncementListBySchoolResponseModel
    {
        public bool isSuccess { get; set; }
        public bool Succeeded { get; set; }
        public string message { get; set; }
        public string statusCode { get; set; }
        public IEnumerable<GetAllAnnouncementListBySchoolDto> data { get; set; }
    }
}
