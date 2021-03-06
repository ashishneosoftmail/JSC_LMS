using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
    public class AnnouncementRepository : BaseRepository<Announcement>, IAnnouncementRepository
    {
        private readonly ILogger _logger;
        public AnnouncementRepository(ApplicationDbContext dbContext, ILogger<Announcement> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        protected override IQueryable<Announcement> GetQueryable()
        {
            return _dbContext.Set<Announcement>().Include(y => y.School).Include(x => x.Teacher).Include(z => z.Subject).Include(a => a.Class).Include(b => b.Section);
        }
        public override async Task<Announcement> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }

        public async Task<IReadOnlyList<Announcement>> PrincipalGetPagedReponseAsyncBySchoolId(int page, int size, int schoolid)
        {
            return await GetQueryable().Where<Announcement>(x => x.SchoolId == schoolid).Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<Announcement>> GetPagedReponseAsyncBySchoolIdClassIdSectionId(int page, int size, int schoolid, int classid, int sectionid)
        {
            return await GetQueryable().Where<Announcement>(x => x.SchoolId == schoolid).Where<Announcement>(x => x.ClassId == classid).Where<Announcement>(x => x.SectionId == sectionid).Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }
    }
}