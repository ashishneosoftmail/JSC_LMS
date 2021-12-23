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
    public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        private readonly ILogger _logger;
        public FeedbackRepository(ApplicationDbContext dbContext, ILogger<Feedback> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        protected override IQueryable<Feedback> GetQueryable()
        {
            return _dbContext.Set<Feedback>().Include(x => x.FeedbackTitle).Include(x => x.Subject).Include(x => x.Students).Include(x => x.Parents);
        }
        public override async Task<Feedback> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }


    }
}
