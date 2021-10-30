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
    public class InstituteRepository : BaseRepository<Institute>, IInstituteRepository
    {
        private readonly ILogger _logger;
        protected readonly ApplicationDbContext _dbContext;
        public InstituteRepository(ApplicationDbContext dbContext, ILogger<Institute> logger) : base(dbContext, logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        protected override IQueryable<Institute> GetQueryable(){
            return _dbContext.Set<Institute>().Include(x => x.City).Include(y => y.State).Include(z => z.Zip);
        }
        public override async Task<Institute> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }
    }
}
