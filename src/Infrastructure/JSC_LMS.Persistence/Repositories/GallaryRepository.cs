using JSC_LMS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using JSC_LMS.Application.Contracts.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
  public  class GallaryRepository : BaseRepository<Gallary>, IGallaryRepository
    {

        private readonly ILogger _logger;
        public GallaryRepository(ApplicationDbContext dbContext, ILogger<Gallary> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        protected override IQueryable<Gallary> GetQueryable()
        {
            return _dbContext.Set<Gallary>().Include(y => y.School).Include(x => x.EventsTable);
        }

        public override async Task<Gallary> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }


    }
}
