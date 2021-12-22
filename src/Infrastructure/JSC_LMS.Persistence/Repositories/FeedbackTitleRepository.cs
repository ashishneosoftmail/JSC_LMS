using JSC_LMS.Application.Contracts.Persistence;
using JSC_LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JSC_LMS.Persistence.Repositories
{
    public class FeedbackTitleRepository : BaseRepository<FeedbackTitle>, IFeedbackTitleRepository
    {
        private readonly ILogger _logger;
        public FeedbackTitleRepository(ApplicationDbContext dbContext, ILogger<FeedbackTitle> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }


        public override async Task<FeedbackTitle> GetByIdAsync(int id)
        {
            return await GetQueryable().FirstOrDefaultAsync(i => i.Id == id);

        }

    }

}