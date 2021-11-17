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

    public class KnowledgeBaseRepository : BaseRepository<KnowledgeBase>, IKnowledgeBaseRepository
    {

        private readonly ILogger _logger;
        public KnowledgeBaseRepository(ApplicationDbContext dbContext, ILogger<KnowledgeBase> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        protected override IQueryable<KnowledgeBase> GetQueryable()
        {
            return _dbContext.Set<KnowledgeBase>().Include(x => x.Category);
        }
        public override async Task<KnowledgeBase> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }
    }
}
