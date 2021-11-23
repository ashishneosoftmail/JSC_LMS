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
   public class FAQRepository : BaseRepository<FAQ>, IFAQRepository
    {
        private readonly ILogger _logger;
        public FAQRepository(ApplicationDbContext dbContext, ILogger<FAQ> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        protected override IQueryable<FAQ> GetQueryable()
        {
            return _dbContext.Set<FAQ>().Include(x => x.Category);
        }
        public override async Task<FAQ> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }
    }
}
