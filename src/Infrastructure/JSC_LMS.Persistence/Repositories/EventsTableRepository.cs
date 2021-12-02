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
    public class EventsTableRepository : BaseRepository<EventsTable>, IEventsTableRepository
    {
        private readonly ILogger _logger;
        public EventsTableRepository(ApplicationDbContext dbContext, ILogger<EventsTable> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        protected override IQueryable<EventsTable> GetQueryable()
        {
            return _dbContext.Set<EventsTable>().Include(y => y.School);
        }
        public override async Task<EventsTable> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }

        public async Task<IReadOnlyList<EventsTable>> PrincipalGetPagedReponseAsyncBySchoolId(int page, int size, int schoolid)
        {
            return await GetQueryable().Where<EventsTable>(x => x.SchoolId == schoolid).Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }
    }
}
