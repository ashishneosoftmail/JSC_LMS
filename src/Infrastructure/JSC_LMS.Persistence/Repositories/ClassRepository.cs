using JSC_LMS.Domain.Entities;
using JSC_LMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
   public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        private readonly ILogger _logger;
        public ClassRepository(ApplicationDbContext dbContext, ILogger<Class> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        protected override IQueryable<Class> GetQueryable()
        {
            return _dbContext.Set<Class>().Include(a => a.School);
        }

        public override async Task<Class> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }
    }
}
