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
   public class AcademicRepository : BaseRepository<Academic>, IAcademicRepository
    {
        private readonly ILogger _logger;
        public AcademicRepository(ApplicationDbContext dbContext, ILogger<Academic> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        protected override IQueryable<Academic> GetQueryable()
        {
            return _dbContext.Set<Academic>().Include(y => y.School).Include(x => x.Teacher).Include(z => z.Subject).Include(a => a.Class).Include(b => b.Section);
        }
        public override async Task<Academic> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }
    }
}
