using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using JSC_LMS.Persistence.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
    public class StudentRepository : BaseRepository<Students>, IStudentRepository
    {
        private readonly ILogger _logger;
        public StudentRepository(ApplicationDbContext dbContext, ILogger<Students> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        protected override IQueryable<Students> GetQueryable()
        {
            return _dbContext.Set<Students>().Include(y => y.State).Include(x => x.City).Include(z => z.Zip).Include(a => a.Class).Include(b=>b.Section);
        }
        public override async Task<Students> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }
    }
}
