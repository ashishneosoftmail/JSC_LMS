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
    public class CircularRepository : BaseRepository<Circular>, ICircularRepository
    {
        private readonly ILogger _logger;
        public CircularRepository(ApplicationDbContext dbContext, ILogger<Circular> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        protected override IQueryable<Circular> GetQueryable()
        {
            return _dbContext.Set<Circular>().Include(y => y.School);
        }
        public override async Task<Circular> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }

        public async Task<IReadOnlyList<Circular>> PrincipalGetPagedReponseAsyncBySchoolId(int page, int size, int schoolid)
        {
            return await GetQueryable().Where<Circular>(x => x.SchoolId == schoolid).Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }
    }
}
