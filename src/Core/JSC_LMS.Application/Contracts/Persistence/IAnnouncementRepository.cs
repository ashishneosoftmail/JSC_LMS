using JSC_LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Application.Contracts.Persistence
{
    public interface IAnnouncementRepository : IAsyncRepository<Announcement>
    {
        Task<IReadOnlyList<Announcement>> PrincipalGetPagedReponseAsyncBySchoolId(int page, int size, int schoolid);

        Task<IReadOnlyList<Announcement>> GetPagedReponseAsyncBySchoolIdClassIdSectionId(int page, int size, int schoolid, int classid, int sectionid);
    }
}
